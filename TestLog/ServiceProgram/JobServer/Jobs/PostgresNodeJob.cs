using Dapper;
using Npgsql;
using Quartz;
using ServiceProgram.Common;
using ServiceProgram.EntityModel;
using ServiceProgram.EntityModel.Target;
using ServiceProgram.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ServiceProgram.JobServer.Jobs
{
    /// <summary>
    /// 
    /// </summary>
    public class PostgresNodeJob : BaseJob.JobBase
    {
        public override string JobName => "PostgresNodeJob";

        public override string Cron => "0 0 15 * * ?";

        protected override void ExcuteJob(IJobExecutionContext context, CancellationTokenSource cancellationSource)
        {
            var time = DateTime.Now;
            var datas = new List<PgTableModel>();
            if (EntityModel.ConfigModel.data_configs != null)
            {
                foreach (var config in EntityModel.ConfigModel.data_configs)
                {
                    if (string.IsNullOrWhiteSpace(config.host) == false)
                    {
                        var databases = config.database.Split(",");
                        foreach (var database in databases)
                        {
                            try
                            {
                                IEnumerable<PgTableModel> hypertable = null;
                                IEnumerable<PgTableModel> row = null;
                                using (var conn = new NpgsqlConnection($"PORT={config.port};DATABASE={database};HOST={config.host};PASSWORD={config.pwd};USER ID={config.user_id}"))
                                {
                                    conn.Open();
                                    hypertable = conn.QueryAsync<PgTableModel>($"SELECT toast_size,table_name,num_chunks,table_size,index_size,total_size FROM timescaledb_information.hypertable;").Result;
                                    row = conn.QueryAsync<PgTableModel>($"SELECT table_name,row_estimate FROM hypertable_approximate_row_count();").Result;
                                    conn.Close();

                                }
                                datas = hypertable.ToList();
                                foreach (var data in datas)
                                {
                                    var a = row.Where(w => w.table_name == data.table_name).FirstOrDefault();
                                    if (a != null)
                                    {
                                        data.row_estimate = a.row_estimate;
                                    }
                                }
                                ServerTargetHelper.PostgresTarget(time, new PostgresTargetModel
                                {
                                    pg_name = config.pg_name,
                                    database = database,
                                    infos = datas.Select(s => new PostgresTargetInfo
                                    {
                                        total_size = UtilHelper.UnitsChange(s.total_size),
                                        toast_size = UtilHelper.UnitsChange(s.toast_size),
                                        index_size = UtilHelper.UnitsChange(s.index_size),
                                        num_chunks = s.num_chunks,
                                        row_estimate = s.row_estimate,
                                        table_size = UtilHelper.UnitsChange(s.table_size),
                                        table_name = s.table_name
                                    }).ToList()
                                });
                            }
                            catch (Exception ex)
                            {
                                UtilHelper.log.Error($"采集服务{config.pg_name}{database}出现报错", ex, true, true);
                            }

                        }
                    }
                }
            }



            //SELECT*FROM timescaledb_information.hypertable
            //WHERE table_schema = 'public' AND table_name = 'metadata';
            //table_schema 超表的架构名称。
            // table_name 超表的表名。
            // table_owner 超表的所有者。
            // num_dimensions 尺寸数。
            // num_chunks 块数。
            // table_size 超表使用的磁盘空间
            // index_size 索引使用的磁盘空间
            // toast_size 烤面包表的磁盘空间
            // total_size 指定表使用的总磁盘空间，包括所有索引和TOAST数据
            //获取单个超表的大概行数。SELECT* FROM hypertable_approximate_row_count('metadata');
            //获取所有超表的近似行数。SELECT* FROM hypertable_approximate_row_count();


        }
    }
}
