using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using IFactory.Domain.Entities;
using IFactory.Domain.Models;
using PagedList;
using IFactory.Common;
using MySql.Data.MySqlClient;

namespace IFactory.DB
{
    public class MyCreateDB : BaseFacade
    {
        public void CreateAlarmTable()
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(@"CREATE TABLE
                                        IF NOT EXISTS `alarm_table` (
	                                        `序号` INT (66) NOT NULL AUTO_INCREMENT,
	                                        `报警内容` VARCHAR (255) NOT NULL,
	                                        `报警时间` DATETIME NOT NULL,
	                                        `dispose_time` DATETIME NOT NULL,
	                                        `craft_name` VARCHAR (255) DEFAULT 'Inspection1',
	                                        `duration` INT (255) DEFAULT 5,
	                                        `alarm_record_did` INT(255) DEFAULT 1342,
	                                        PRIMARY KEY (`序号`)
                                        ) ENGINE = INNODB DEFAULT CHARSET = utf8;");
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }

        public void CreateLoadUnloadProductionTable()
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(@"CREATE TABLE
                                        IF NOT EXISTS `loadunloadproduction` (
	                                        `barcode` VARCHAR (255) NOT NULL,
	                                        `inputTime` DATETIME,
	                                        PRIMARY KEY (`barcode`)
                                        ) ENGINE = INNODB DEFAULT CHARSET = utf8;");
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }

        public void CreateCapacityOfProductinoTable()
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(@"CREATE TABLE
                                        IF NOT EXISTS `capacityofproduction` (
	                                        `时间` DATE,
	                                        `产能` VARCHAR (255) NOT NULL,
	                                        PRIMARY KEY (`时间`)
                                        ) ENGINE = INNODB DEFAULT CHARSET = utf8;");
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }
    } 
}
