/*
Navicat MySQL Data Transfer

Source Server         : local
Source Server Version : 50635
Source Host           : localhost:3306
Source Database       : ifactory

Target Server Type    : MYSQL
Target Server Version : 50635
File Encoding         : 65001

Date: 2018-05-17 09:58:21
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for alarm_fields
-- ----------------------------
DROP TABLE IF EXISTS `alarm_fields`;
CREATE TABLE `alarm_fields` (
  `AlarmFieldId` int(11) NOT NULL AUTO_INCREMENT COMMENT '报警字段主键',
  `FieldName` varchar(50) NOT NULL COMMENT '报警字段名称',
  `FieldDescription` varchar(50) NOT NULL COMMENT '报警字段描述',
  PRIMARY KEY (`AlarmFieldId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for alarm_location_image
-- ----------------------------
DROP TABLE IF EXISTS `alarm_location_image`;
CREATE TABLE `alarm_location_image` (
  `did` int(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `path` varchar(255) NOT NULL COMMENT '图片路径',
  PRIMARY KEY (`did`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for alarm_record
-- ----------------------------
DROP TABLE IF EXISTS `alarm_record`;
CREATE TABLE `alarm_record` (
  `alarm_record_did` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `rule_did` varchar(20) NOT NULL COMMENT '规则编号',
  `facility_did` int(11) NOT NULL COMMENT '设备编号',
  `alarm_time` datetime NOT NULL COMMENT '报警时间',
  `alarm_count` int(11) DEFAULT NULL COMMENT '报警次数',
  `dispose_state` int(11) NOT NULL COMMENT '处理状态  默认值0（0：未处理，1：处理中,2：已处理)',
  `dispose_time` datetime NOT NULL COMMENT '处理时间',
  `handler` varchar(50) DEFAULT NULL COMMENT '处理人',
  `duration` int(11) DEFAULT NULL COMMENT '报警时长',
  `address` varchar(255) DEFAULT NULL COMMENT '报警地址',
  `remark` varchar(255) DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`alarm_record_did`),
  KEY `fk_alarm_record_idx` (`rule_did`),
  KEY `idx_alarm_time` (`alarm_time`),
  CONSTRAINT `rule_id` FOREIGN KEY (`rule_did`) REFERENCES `alarm_rule` (`rule_did`)
) ENGINE=InnoDB AUTO_INCREMENT=33243 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for alarm_rule
-- ----------------------------
DROP TABLE IF EXISTS `alarm_rule`;
CREATE TABLE `alarm_rule` (
  `rule_did` varchar(20) NOT NULL COMMENT '主键',
  `alarm_type_did` varchar(20) NOT NULL COMMENT '报警类型编号',
  `solution_did` int(20) DEFAULT NULL COMMENT '解决方案编号',
  `solution_image_did` int(20) DEFAULT NULL COMMENT '解决方案图片路径编号',
  `alarm_location_image_did` int(20) DEFAULT NULL COMMENT '报警点图片路径编号',
  `craft_did` int(20) NOT NULL COMMENT '工艺编号',
  `unit_did` int(20) NOT NULL COMMENT '部件编号',
  `level` int(11) DEFAULT '0' COMMENT '报警等级',
  `alarm_content` varchar(1000) DEFAULT NULL COMMENT '报警内容，不得超过400个汉字',
  `alarm_reason` varchar(1000) DEFAULT NULL COMMENT '报警原因，不得超过400个汉字',
  PRIMARY KEY (`rule_did`),
  KEY `fk_alarm_rule_alerm_type_idx` (`alarm_type_did`),
  KEY `fk_alarm_rule_alerm_location_image_idx` (`alarm_location_image_did`),
  KEY `fk_alarm_rule_solution_idx` (`solution_did`),
  KEY `fk_alarm_rule_solution_image_idx` (`solution_image_did`),
  KEY `fk_alarm_rule_unit_idx` (`unit_did`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for alarm_temporary
-- ----------------------------
DROP TABLE IF EXISTS `alarm_temporary`;
CREATE TABLE `alarm_temporary` (
  `alarm_temporary_did` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `rule_did` varchar(20) NOT NULL COMMENT '报警规则编号',
  `facility_did` int(11) NOT NULL COMMENT '设备编号',
  `alarm_time` datetime NOT NULL COMMENT '报警时间',
  `dispose_state` int(11) DEFAULT NULL COMMENT '处理状态 默认值0（0：未处理，1：处理中,2：已处理)',
  `dispose_time` datetime DEFAULT NULL COMMENT '处理时间',
  `handler` varchar(50) DEFAULT NULL COMMENT '处理人',
  `duration` int(11) DEFAULT NULL COMMENT '报警时长',
  `address` varchar(255) DEFAULT NULL COMMENT '报警地址',
  `remark` varchar(255) DEFAULT NULL COMMENT '备注',
  `alarm_id` int(11) DEFAULT NULL,
  `create_by` varchar(50) NOT NULL COMMENT '创建人',
  `create_date` datetime NOT NULL DEFAULT '0000-00-00 00:00:00' COMMENT '创建日期',
  `update_by` varchar(50) DEFAULT NULL COMMENT '修改人',
  `update_date` datetime DEFAULT NULL COMMENT '修改日期',
  `marks` int(11) DEFAULT '0' COMMENT '标识（0未发，1发送）',
  PRIMARY KEY (`alarm_temporary_did`),
  KEY `rule_id1` (`rule_did`),
  CONSTRAINT `rule_id1` FOREIGN KEY (`rule_did`) REFERENCES `alarm_rule` (`rule_did`)
) ENGINE=InnoDB AUTO_INCREMENT=68503 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for alarm_times
-- ----------------------------
DROP TABLE IF EXISTS `alarm_times`;
CREATE TABLE `alarm_times` (
  `iden` int(11) NOT NULL AUTO_INCREMENT,
  `batterytype` varchar(255) DEFAULT NULL COMMENT '电池类型',
  `SN` varchar(255) DEFAULT NULL COMMENT 'SN',
  `datetime` datetime DEFAULT NULL COMMENT '生产时间',
  `alarmtype` varchar(255) DEFAULT NULL COMMENT '电池类型',
  PRIMARY KEY (`iden`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for alarm_type
-- ----------------------------
DROP TABLE IF EXISTS `alarm_type`;
CREATE TABLE `alarm_type` (
  `did` varchar(20) NOT NULL COMMENT '主键',
  `type` varchar(50) NOT NULL COMMENT '报警类型名称',
  PRIMARY KEY (`did`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for autoinspection1_facility_production_data
-- ----------------------------
DROP TABLE IF EXISTS `autoinspection1_facility_production_data`;
CREATE TABLE `autoinspection1_facility_production_data` (
  `iden` int(11) NOT NULL AUTO_INCREMENT,
  `ProductNo` varchar(50) NOT NULL COMMENT '产品型号',
  `batteryBarCode` varchar(50) NOT NULL COMMENT '电芯条码',
  `device_group_did` int(11) NOT NULL COMMENT '设备组编号',
  `process_did` int(11) NOT NULL COMMENT '工序编号',
  `facility_did` int(11) NOT NULL COMMENT '设备编号',
  `No` varchar(32) NOT NULL COMMENT '序号',
  `StartDate` datetime NOT NULL COMMENT '生产日期',
  `result` int(11) NOT NULL COMMENT '测试结果',
  `Back_return` int(11) NOT NULL COMMENT '背面缺陷检测结果',
  `Back_errcode` int(11) NOT NULL COMMENT '背面缺陷检测错误码',
  `Front_return` int(11) NOT NULL COMMENT '正面缺陷检测结果',
  `Front_errcode` int(11) NOT NULL COMMENT '正面缺陷检测错误码',
  `Hipot_return` varchar(11) NOT NULL COMMENT 'Hipot检测结果',
  `Hipot_errcode` int(11) NOT NULL COMMENT 'Hipot检测错误码',
  `Sidestrip_Height` double NOT NULL DEFAULT '0' COMMENT '侧封高度',
  `Sidestrip_Width` double NOT NULL DEFAULT '0' COMMENT '侧封宽度',
  `Topstrip_Height` double NOT NULL DEFAULT '0' COMMENT '顶封宽度',
  `MainBody_Width` double NOT NULL DEFAULT '0' COMMENT '主体宽度',
  `MainBody_Height` double NOT NULL DEFAULT '0' COMMENT '主体长度',
  `Distance_Between_Tabs` double NOT NULL DEFAULT '0' COMMENT '极耳中心距',
  `Distance_Between_Tab1_and_left_edge` double NOT NULL DEFAULT '0' COMMENT '左极耳左边距',
  `Distance_Between_Tab2_and_left_edge` double NOT NULL COMMENT '右极耳左边距',
  `BagArea_width` double NOT NULL DEFAULT '0' COMMENT '气袋宽度',
  `TabALToSlotDistanceRight` double NOT NULL DEFAULT '0' COMMENT '右极耳与槽位印',
  `TabNIToSlotDistanceLeft` double NOT NULL DEFAULT '0' COMMENT '左极耳与槽位印',
  `SealantHeightOfLeft1` double NOT NULL DEFAULT '0' COMMENT '左Sealant高度1',
  `SealantHeightOfLeft2` double NOT NULL DEFAULT '0' COMMENT '左Sealant高度2',
  `SealantHeightOfRight1` double NOT NULL DEFAULT '0' COMMENT '右Sealant高度1',
  `SealantHeightOfRight2` double NOT NULL DEFAULT '0' COMMENT '右Sealant高度2',
  `SealantToSlotDistanceLeft` double NOT NULL DEFAULT '0' COMMENT '左Sealant与槽位印距离',
  `SealantToSlotDistanceRight` double NOT NULL DEFAULT '0' COMMENT '右Sealant与槽位印距离',
  `Topseal_Height_2nd` double NOT NULL DEFAULT '0',
  `measmode` int(11) NOT NULL DEFAULT '0' COMMENT '测量模式',
  `SidePoint1` double NOT NULL DEFAULT '0' COMMENT '边封位置点1',
  `SidePoint2` double NOT NULL DEFAULT '0' COMMENT '边封位置点2',
  `SidePoint3` double NOT NULL DEFAULT '0' COMMENT '边封位置点3',
  `TopPoint1` double NOT NULL DEFAULT '0' COMMENT '顶封位置点1',
  `TopPoint2` double NOT NULL DEFAULT '0' COMMENT '顶封位置点2',
  `TopPoint3` double NOT NULL DEFAULT '0' COMMENT '顶封位置点3',
  `TabPoint1` double NOT NULL DEFAULT '0' COMMENT '顶封极耳位置点1',
  `TabPoint2` double NOT NULL DEFAULT '0' COMMENT '顶封极耳位置点2',
  `marks` int(11) NOT NULL DEFAULT '-99' COMMENT '标记',
  `create_by` varchar(64) DEFAULT NULL COMMENT '创建者',
  `create_date` datetime DEFAULT NULL COMMENT '创建日期',
  `update_by` varchar(64) DEFAULT NULL COMMENT '更新者',
  `update_date` datetime DEFAULT NULL COMMENT '更新日期',
  `remake` varchar(255) DEFAULT NULL COMMENT '备注',
  `end_product_no` varchar(64) DEFAULT NULL COMMENT '成品编码',
  `HIPOT_TEST_NO` varchar(5) DEFAULT NULL COMMENT 'HIPOT测试',
  `HIPOT_PRESS_NO` varchar(5) DEFAULT NULL COMMENT 'HIPOT测试',
  `GMECHS_INFO1` varchar(10) DEFAULT NULL COMMENT 'Hipot阻值(兆欧)',
  `shift` varchar(10) DEFAULT NULL COMMENT '班别',
  `operator` varchar(32) DEFAULT NULL COMMENT '操作人员（验证信息中的人员信息）',
  `machine_no` varchar(16) DEFAULT NULL COMMENT '设备编号（机器上的铭牌）',
  PRIMARY KEY (`iden`),
  KEY `index_name` (`batteryBarCode`),
  KEY `index_marks` (`marks`),
  KEY `index_StartDate` (`StartDate`),
  KEY `index_DeviceGroupDID` (`device_group_did`)
) ENGINE=InnoDB AUTO_INCREMENT=83389 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for autoinspection1_run_total_collect
-- ----------------------------
DROP TABLE IF EXISTS `autoinspection1_run_total_collect`;
CREATE TABLE `autoinspection1_run_total_collect` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `craft_did` int(11) NOT NULL COMMENT '工艺ID',
  `device_group_did` int(11) NOT NULL COMMENT '设备组ID',
  `alltime` bigint(20) NOT NULL DEFAULT '720' COMMENT '总时间（该设备组12*60分钟）',
  `MCCollectDDate` datetime NOT NULL COMMENT '设备采集时间',
  `RuningTime` bigint(20) NOT NULL COMMENT '累计运行时间',
  `WaitTime` bigint(20) NOT NULL COMMENT '待机时间',
  `StopTime` bigint(20) NOT NULL COMMENT '停机时间',
  `NGcount` int(11) NOT NULL COMMENT 'NG数量',
  `celltotal` int(11) NOT NULL COMMENT '电芯总数量',
  `datetime` datetime NOT NULL COMMENT '记录日期',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3113 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for autoinspection1_run_total_collect_real
-- ----------------------------
DROP TABLE IF EXISTS `autoinspection1_run_total_collect_real`;
CREATE TABLE `autoinspection1_run_total_collect_real` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `craft_did` int(11) NOT NULL COMMENT '工艺ID',
  `device_group_did` int(11) NOT NULL COMMENT '设备组ID',
  `alltime` bigint(20) NOT NULL DEFAULT '720' COMMENT '总时间（该设备组12*60分钟）',
  `MCCollectDDate` datetime NOT NULL COMMENT '设备采集时间',
  `RuningTime` bigint(20) NOT NULL COMMENT '累计运行时间',
  `WaitTime` bigint(20) NOT NULL COMMENT '待机时间',
  `StopTime` bigint(20) NOT NULL COMMENT '停机时间',
  `NGcount` int(11) NOT NULL COMMENT 'NG数量',
  `celltotal` int(11) NOT NULL COMMENT '电芯总数量',
  `datetime` datetime NOT NULL COMMENT '记录日期',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3113 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for autoinspection2_facility_production_data
-- ----------------------------
DROP TABLE IF EXISTS `autoinspection2_facility_production_data`;
CREATE TABLE `autoinspection2_facility_production_data` (
  `iden` int(11) NOT NULL AUTO_INCREMENT,
  `ProductNo` varchar(50) NOT NULL COMMENT '产品型号',
  `batteryBarCode` varchar(50) NOT NULL COMMENT '电芯条码',
  `device_group_did` int(11) NOT NULL COMMENT '设备组编号',
  `No` varchar(32) NOT NULL COMMENT '序号',
  `StartDate` datetime NOT NULL COMMENT '生产日期',
  `result` int(11) NOT NULL COMMENT '测试结果',
  `Back_return` int(11) NOT NULL COMMENT '背面缺陷检测结果',
  `Back_errcode` int(11) NOT NULL COMMENT '背面缺陷检测错误码',
  `Front_return` int(11) NOT NULL COMMENT '正面缺陷检测结果',
  `Front_errcode` int(11) NOT NULL COMMENT '正面缺陷检测错误码',
  `MainBody_Width_Top` double DEFAULT '0' COMMENT '主体宽度',
  `MainBody_Width_Bottom` double DEFAULT '0' COMMENT '主体长度',
  `MainBody_Height` double DEFAULT '0' COMMENT '顶峰宽度',
  `Topseal_Height` double DEFAULT '0' COMMENT '左极耳左边距',
  `Distance_Between_Tab1_and_left_edge` double DEFAULT NULL COMMENT '右极耳左边距',
  `Distance_Between_Tab2_and_left_edge` double DEFAULT NULL COMMENT '两Tab间距',
  `Distance_Between_Tab` double DEFAULT NULL COMMENT '左Tab高度',
  `Side_LeftFoldingHight_Top` double DEFAULT NULL COMMENT '右Tab高度',
  `Side_LeftFoldingHight_Bottom` double DEFAULT NULL COMMENT '左Tab宽度',
  `Side_RightFoldingHight_Top` double DEFAULT NULL COMMENT '右Tab宽度',
  `Side_RightFoldingHight_Bottom` double DEFAULT NULL COMMENT '左边厚度',
  `marks` int(11) NOT NULL DEFAULT '-99' COMMENT '标记',
  `create_by` varchar(64) DEFAULT NULL COMMENT '创建者',
  `create_date` datetime DEFAULT NULL COMMENT '创建日期',
  `update_by` varchar(64) DEFAULT NULL COMMENT '更新者',
  `update_date` datetime DEFAULT NULL COMMENT '更新日期',
  `remake` varchar(255) DEFAULT NULL COMMENT '备注',
  `end_product_no` varchar(64) DEFAULT NULL COMMENT '成品编码',
  `shift` varchar(10) DEFAULT NULL COMMENT '班别',
  `operator` varchar(32) DEFAULT NULL COMMENT '操作人员（验证信息中的人员信息）',
  `machine_no` varchar(16) DEFAULT NULL COMMENT '设备编号（机器上的铭牌）',
  `Process_did` int(11) NOT NULL COMMENT '工序号',
  PRIMARY KEY (`iden`),
  KEY `index_marks` (`marks`),
  KEY `index_batteryBarCode` (`batteryBarCode`),
  KEY `index_StartDate` (`StartDate`),
  KEY `index_DeviceGroupDID` (`device_group_did`)
) ENGINE=InnoDB AUTO_INCREMENT=2177 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for autoinspection2_run_total_collect
-- ----------------------------
DROP TABLE IF EXISTS `autoinspection2_run_total_collect`;
CREATE TABLE `autoinspection2_run_total_collect` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `craft_did` int(11) NOT NULL COMMENT '工艺ID',
  `device_group_did` int(11) NOT NULL COMMENT '设备组ID',
  `alltime` bigint(20) NOT NULL DEFAULT '720' COMMENT '总时间（该设备组12*60分钟）',
  `MCCollectDDate` datetime NOT NULL COMMENT '设备采集时间',
  `RuningTime` bigint(20) NOT NULL COMMENT '累计运行时间',
  `WaitTime` bigint(20) NOT NULL COMMENT '待机时间',
  `StopTime` bigint(20) NOT NULL COMMENT '停机时间',
  `NGcount` int(11) NOT NULL COMMENT 'NG数量',
  `celltotal` int(11) NOT NULL COMMENT '电芯总数量',
  `datetime` datetime NOT NULL COMMENT '记录时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for autoinspection2_run_total_collect_real
-- ----------------------------
DROP TABLE IF EXISTS `autoinspection2_run_total_collect_real`;
CREATE TABLE `autoinspection2_run_total_collect_real` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `craft_did` int(11) NOT NULL COMMENT '工艺ID',
  `device_group_did` int(11) NOT NULL COMMENT '设备组ID',
  `alltime` bigint(20) NOT NULL DEFAULT '720' COMMENT '总时间（该设备组12*60分钟）',
  `MCCollectDDate` datetime NOT NULL COMMENT '设备采集时间',
  `RuningTime` bigint(20) NOT NULL COMMENT '累计运行时间',
  `WaitTime` bigint(20) NOT NULL COMMENT '待机时间',
  `StopTime` bigint(20) NOT NULL COMMENT '停机时间',
  `NGcount` int(11) NOT NULL COMMENT 'NG数量',
  `celltotal` int(11) NOT NULL COMMENT '电芯总数量',
  `datetime` datetime NOT NULL COMMENT '记录时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for code_generators
-- ----------------------------
DROP TABLE IF EXISTS `code_generators`;
CREATE TABLE `code_generators` (
  `Prefix` varchar(20) NOT NULL,
  `SerialNo` int(11) NOT NULL,
  PRIMARY KEY (`Prefix`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for craft
-- ----------------------------
DROP TABLE IF EXISTS `craft`;
CREATE TABLE `craft` (
  `craft_did` int(20) NOT NULL COMMENT '主键',
  `craft_no` varchar(255) NOT NULL COMMENT '逻辑编号',
  `craft_name` varchar(255) NOT NULL COMMENT '按钮名称',
  PRIMARY KEY (`craft_did`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for craft_probably
-- ----------------------------
DROP TABLE IF EXISTS `craft_probably`;
CREATE TABLE `craft_probably` (
  `Iden` int(11) NOT NULL AUTO_INCREMENT,
  `craft_did` int(11) NOT NULL COMMENT '工艺编号',
  `nowYield` int(11) NOT NULL COMMENT '当前产量',
  `targetYield` int(11) NOT NULL DEFAULT '9999' COMMENT '目标产量',
  `Operator` varchar(50) NOT NULL COMMENT '操作员',
  `OKCount` int(11) NOT NULL COMMENT '良品',
  `NGCount` int(11) NOT NULL COMMENT '不良品',
  `device_group_did` int(11) NOT NULL COMMENT '设备组ID',
  `PPM` double(11,0) NOT NULL COMMENT 'PPM',
  `Resource` varchar(50) NOT NULL COMMENT '设备编号',
  `time` datetime NOT NULL COMMENT '时间',
  PRIMARY KEY (`Iden`),
  KEY `idx_craft_device_group` (`craft_did`,`device_group_did`),
  KEY `idx_time` (`time`)
) ENGINE=InnoDB AUTO_INCREMENT=4480 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for device_group
-- ----------------------------
DROP TABLE IF EXISTS `device_group`;
CREATE TABLE `device_group` (
  `device_group_did` int(11) NOT NULL,
  `device_group_no` varchar(50) DEFAULT NULL,
  `device_group_name` varchar(255) DEFAULT NULL,
  `craft_did` int(11) DEFAULT NULL,
  `isready` int(11) NOT NULL DEFAULT '1' COMMENT '各设备组准备状态，0：准备OK；1：未准备好；',
  `update_date` datetime NOT NULL COMMENT '状态更新时间',
  PRIMARY KEY (`device_group_did`),
  KEY `idx_group_no` (`device_group_no`),
  KEY `fki_group_did` (`device_group_did`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for facility_info
-- ----------------------------
DROP TABLE IF EXISTS `facility_info`;
CREATE TABLE `facility_info` (
  `facility_did` int(11) NOT NULL COMMENT '主键',
  `MMName` varchar(255) NOT NULL COMMENT '设备/工序名称',
  `process_did` int(11) NOT NULL COMMENT '工序/设备编号',
  `device_group_did` int(11) NOT NULL,
  `State` int(11) NOT NULL COMMENT '设备状态 （0：工作、1：待机、2：停机、）',
  `HeartbeatTime` datetime NOT NULL COMMENT '心跳包，每30秒更新一次时间',
  PRIMARY KEY (`facility_did`),
  KEY `idx_device_group_facility` (`device_group_did`,`facility_did`) USING BTREE,
  KEY `fdi_facility_did` (`facility_did`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for factoryinfo
-- ----------------------------
DROP TABLE IF EXISTS `factoryinfo`;
CREATE TABLE `factoryinfo` (
  `factoryID` varchar(255) NOT NULL DEFAULT '' COMMENT '工厂编号',
  `fano` varchar(255) NOT NULL DEFAULT '' COMMENT '设备资产编号',
  `end_product_no` varchar(255) NOT NULL COMMENT '成品编码',
  `time` datetime DEFAULT NULL COMMENT '验证时间',
  PRIMARY KEY (`factoryID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for historypartapply
-- ----------------------------
DROP TABLE IF EXISTS `historypartapply`;
CREATE TABLE `historypartapply` (
  `paId` int(10) NOT NULL AUTO_INCREMENT,
  `paName` varchar(50) DEFAULT NULL,
  `predictNum` int(10) DEFAULT NULL,
  `useNum` int(10) DEFAULT NULL,
  `userId` int(10) DEFAULT NULL,
  `renewalTime` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`paId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for host_config
-- ----------------------------
DROP TABLE IF EXISTS `host_config`;
CREATE TABLE `host_config` (
  `Iden` int(11) NOT NULL AUTO_INCREMENT,
  `craft_did` int(11) DEFAULT NULL COMMENT '主键，工艺ID',
  `device_group_did` int(11) DEFAULT NULL COMMENT '设备组ID',
  `MMSeq` varchar(50) DEFAULT NULL COMMENT '设备内部序号',
  `MMname` varchar(255) DEFAULT NULL COMMENT '设备内部名称',
  `MMIP` varchar(50) DEFAULT NULL COMMENT '设备IP地址',
  `MMPort` varchar(50) DEFAULT NULL COMMENT '设备端口号',
  `PLC1_name` varchar(50) DEFAULT NULL COMMENT '连接的PLC1名称',
  `PLC1_address` varchar(50) DEFAULT NULL COMMENT '连接的PLC1的IP或串口端口',
  `PLC2_name` varchar(50) DEFAULT NULL COMMENT '连接的PLC2名称',
  `PLC2_address` varchar(50) DEFAULT NULL COMMENT '连接的PLC2的IP或串口端口',
  `PLC3_name` varchar(50) DEFAULT NULL COMMENT '连接的PLC3名称',
  `PLC3_address` varchar(50) DEFAULT NULL COMMENT '连接的PLC3的IP或串口端口',
  `DB_total` int(11) DEFAULT NULL COMMENT '数据库数量',
  `MMIsUse` varchar(50) DEFAULT NULL COMMENT '设备是否已启用',
  `MMClearAddr` varchar(50) DEFAULT NULL COMMENT '设备清除报警地址',
  `MMRestAddr` varchar(50) DEFAULT NULL COMMENT '设备重置地址',
  `Config_time` datetime DEFAULT NULL COMMENT '配置时间',
  `Remark` varchar(250) DEFAULT NULL COMMENT '备注信息',
  PRIMARY KEY (`Iden`)
) ENGINE=InnoDB AUTO_INCREMENT=1248 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for managers
-- ----------------------------
DROP TABLE IF EXISTS `managers`;
CREATE TABLE `managers` (
  `ManagerId` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ManagerName` varchar(50) NOT NULL COMMENT '姓名',
  `UserName` varchar(50) NOT NULL COMMENT '用户名',
  `Password` varchar(50) NOT NULL COMMENT '密码',
  PRIMARY KEY (`ManagerId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for onekeychange
-- ----------------------------
DROP TABLE IF EXISTS `onekeychange`;
CREATE TABLE `onekeychange` (
  `model` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for onekey_control
-- ----------------------------
DROP TABLE IF EXISTS `onekey_control`;
CREATE TABLE `onekey_control` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `order_state` int(11) NOT NULL DEFAULT '2' COMMENT '整线控制指令（0：暂停，1：启动，2 : 什么都不做）',
  `line` varchar(50) DEFAULT NULL COMMENT '产线编号',
  `update_date` datetime NOT NULL COMMENT '指令更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for permissions
-- ----------------------------
DROP TABLE IF EXISTS `permissions`;
CREATE TABLE `permissions` (
  `PermissionId` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `PermissionName` varchar(20) NOT NULL COMMENT '菜单名称',
  `PermissionCode` varchar(50) DEFAULT NULL COMMENT '菜单编码',
  `Remark` varchar(50) DEFAULT NULL COMMENT '备注',
  `DisplayOrder` int(11) NOT NULL COMMENT '显示顺序',
  `ParentId` int(11) DEFAULT NULL COMMENT '父菜单ID',
  `Depth` int(11) NOT NULL COMMENT '层级',
  PRIMARY KEY (`PermissionId`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for plc_state
-- ----------------------------
DROP TABLE IF EXISTS `plc_state`;
CREATE TABLE `plc_state` (
  `plc_did` int(11) NOT NULL COMMENT '主键',
  `plc_name` varchar(50) NOT NULL COMMENT 'PLC名称',
  `state` int(11) NOT NULL COMMENT '状态，默认0（0：已连接、1：未连接）',
  `craft_did` int(11) NOT NULL COMMENT '工序编号',
  PRIMARY KEY (`plc_did`),
  UNIQUE KEY `idx_craftid_plc_name` (`plc_did`,`plc_name`) USING BTREE
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for process
-- ----------------------------
DROP TABLE IF EXISTS `process`;
CREATE TABLE `process` (
  `process_did` int(11) NOT NULL COMMENT '主键',
  `process_no` varchar(255) NOT NULL COMMENT '逻辑编号',
  `process_name` varchar(255) NOT NULL COMMENT '工序名称',
  `craft_did` int(11) NOT NULL COMMENT '工艺编号',
  PRIMARY KEY (`process_did`),
  KEY `idx_craft_did` (`craft_did`) USING BTREE,
  KEY `idx_process_did` (`process_did`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for production_alldata
-- ----------------------------
DROP TABLE IF EXISTS `production_alldata`;
CREATE TABLE `production_alldata` (
  `did` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `packing_production_AllData_did` int(11) DEFAULT NULL COMMENT 'packing工艺设备总生产数据编号',
  `autoInspection_production_AllData_did` int(11) DEFAULT NULL COMMENT 'autoInspection工艺设备总生产数据编号',
  `myLar_production_AllData_did` int(11) DEFAULT NULL COMMENT 'myLar工艺设备总生产数据编号',
  `MIB_production_AllData_did` int(11) DEFAULT NULL COMMENT 'MIB工艺设备总生产数据编号',
  `injection_production_AllData_did` int(11) DEFAULT NULL COMMENT 'injection工艺设备总生产数据编号',
  `freeBaking_production_AllData_did` int(11) DEFAULT NULL COMMENT 'freeBaking工艺设备总生产数据编号',
  `PIEF_production_AllData_did` int(11) DEFAULT NULL COMMENT 'PIEF工艺设备总生产数据编号',
  `degassing_production_AllData_did` int(11) DEFAULT NULL COMMENT 'degassing工艺设备总生产数据编号',
  `FEF_production_AllData_did` int(11) DEFAULT NULL COMMENT 'FEF工艺设备总生产数据编号',
  `autoInspection2_production_AllData_did` int(11) DEFAULT NULL COMMENT 'autoInspection2工艺设备总生产数据编号',
  `OCV1_production_AllData_did` int(11) DEFAULT NULL COMMENT 'OCV1工艺设备总生产数据编号',
  `OCVB_production_AllData_did` int(11) DEFAULT NULL COMMENT 'OCVB工艺设备总生产数据编号',
  `XRAY_production_AllData_did` int(11) DEFAULT NULL COMMENT 'XRAY工艺设备总生产数据编号',
  `FQI_production_AllData_did` int(11) DEFAULT NULL COMMENT 'FQI工艺设备总生产数据编号',
  `AVI_production_AllData_did` int(11) DEFAULT NULL COMMENT 'AVI工艺设备总生产数据编号',
  `batteryBarCode` varchar(50) DEFAULT NULL COMMENT '电池条码',
  `batteriesBarCode` varchar(50) DEFAULT NULL COMMENT '电芯条码',
  `startTime` datetime NOT NULL COMMENT '该电芯开始生产时间',
  `endTime` datetime DEFAULT NULL COMMENT '该电芯完成生产时间',
  `create_by` varchar(64) DEFAULT NULL COMMENT '创建者',
  `create_date` datetime DEFAULT NULL COMMENT '创建日期',
  `update_by` varchar(64) DEFAULT NULL COMMENT '更新者',
  `update_date` datetime DEFAULT NULL COMMENT '更新日期',
  `remarks` varchar(500) DEFAULT NULL COMMENT '备注',
  `del_flag` char(6) DEFAULT NULL COMMENT '删除标记',
  PRIMARY KEY (`did`),
  KEY `fk_production_alldata1_idx` (`autoInspection2_production_AllData_did`),
  KEY `fk_production_alldata2_idx` (`autoInspection_production_AllData_did`),
  KEY `fk_production_alldata3_idx` (`degassing_production_AllData_did`),
  KEY `fk_production_alldata4_idx` (`AVI_production_AllData_did`),
  KEY `fk_production_alldata5_idx` (`FEF_production_AllData_did`),
  KEY `fk_production_alldata6_idx` (`freeBaking_production_AllData_did`),
  KEY `fk_production_alldata7_idx` (`injection_production_AllData_did`),
  KEY `fk_production_alldata8_idx` (`MIB_production_AllData_did`),
  KEY `fk_production_alldata9_idx` (`myLar_production_AllData_did`),
  KEY `fk_production_alldata10_idx` (`OCV1_production_AllData_did`),
  KEY `fk_production_alldata11_idx` (`OCVB_production_AllData_did`),
  KEY `fk_production_alldata12_idx` (`packing_production_AllData_did`),
  KEY `fk_production_alldata13_idx` (`PIEF_production_AllData_did`),
  KEY `fk_production_alldata14_idx` (`XRAY_production_AllData_did`),
  KEY `fk_production_alldata15_idx` (`FQI_production_AllData_did`)
) ENGINE=InnoDB AUTO_INCREMENT=281 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for production_line_probably
-- ----------------------------
DROP TABLE IF EXISTS `production_line_probably`;
CREATE TABLE `production_line_probably` (
  `did` int(11) NOT NULL DEFAULT '0' COMMENT '主键',
  `name` varchar(50) NOT NULL COMMENT '产线名称',
  `production_type` varchar(50) NOT NULL COMMENT '生产型号',
  `nowYield` int(11) NOT NULL COMMENT '当前产量',
  `targetYield` int(11) NOT NULL COMMENT '目标产量',
  `userName` varchar(50) NOT NULL COMMENT '当前用户',
  PRIMARY KEY (`did`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for production_type
-- ----------------------------
DROP TABLE IF EXISTS `production_type`;
CREATE TABLE `production_type` (
  `did` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ProductNo` varchar(50) DEFAULT NULL COMMENT '生产型号',
  `MinWeight` decimal(8,2) DEFAULT NULL COMMENT '最小重量',
  `MaxWeight` decimal(8,2) DEFAULT NULL COMMENT '最大重量',
  `MinScope` decimal(8,2) DEFAULT NULL COMMENT '最小范围',
  `MaxScope` decimal(8,2) DEFAULT NULL COMMENT '最大范围',
  `BarCodeLen` int(11) DEFAULT NULL COMMENT '条码长度',
  `PrefixLen` int(11) DEFAULT NULL COMMENT '条码前缀长度',
  `PrefixData` varchar(50) DEFAULT NULL COMMENT '条码前缀',
  `DefaultBarCode` varchar(50) DEFAULT NULL COMMENT '默认条码',
  `facility_did` int(11) NOT NULL COMMENT '设备编号',
  `time` datetime DEFAULT NULL COMMENT '时间',
  PRIMARY KEY (`did`),
  KEY `fk_production_type_facility_info_idx` (`facility_did`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for productmodel
-- ----------------------------
DROP TABLE IF EXISTS `productmodel`;
CREATE TABLE `productmodel` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `productNo` varchar(200) NOT NULL DEFAULT '0' COMMENT '生产型号',
  `productName` varchar(200) NOT NULL DEFAULT '0' COMMENT '型号名称',
  `recipeid` int(11) DEFAULT '0' COMMENT '配方ID',
  `creater` varchar(50) NOT NULL DEFAULT '0' COMMENT '创建者',
  `alter` varchar(50) DEFAULT '0' COMMENT '更改者',
  `createtime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updatetime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更改时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='产品型号';

-- ----------------------------
-- Table structure for productmodelhistory
-- ----------------------------
DROP TABLE IF EXISTS `productmodelhistory`;
CREATE TABLE `productmodelhistory` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `productionNo` varchar(100) NOT NULL DEFAULT '0' COMMENT '产品型号',
  `productName` varchar(100) NOT NULL DEFAULT '0' COMMENT '型号名称',
  `recipeid` int(11) DEFAULT '0' COMMENT '配方ID',
  `alter` varchar(50) DEFAULT '0' COMMENT '更改者',
  `createtime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '记录时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='产品型号资料历史数据';

-- ----------------------------
-- Table structure for quickwearparthistoryinfo
-- ----------------------------
DROP TABLE IF EXISTS `quickwearparthistoryinfo`;
CREATE TABLE `quickwearparthistoryinfo` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `theQuantity` int(11) NOT NULL,
  `lifeSpan` int(11) NOT NULL,
  `changeTotalNumber` int(11) NOT NULL,
  `changeDate` datetime NOT NULL,
  `changeUser` varchar(20) DEFAULT NULL,
  `figure1` varchar(50) DEFAULT NULL,
  `figure2` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COMMENT='易损件更换历史记录';

-- ----------------------------
-- Table structure for quickwearpartinfo
-- ----------------------------
DROP TABLE IF EXISTS `quickwearpartinfo`;
CREATE TABLE `quickwearpartinfo` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `theQuantity` int(11) NOT NULL,
  `lifeSpan` int(11) NOT NULL,
  `changeTotalNumber` int(11) NOT NULL,
  `changeDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `changeUser` varchar(20) DEFAULT NULL,
  `plcAddress` varchar(10) DEFAULT NULL,
  `figure1` varchar(50) DEFAULT NULL,
  `figure2` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='易损件';

-- ----------------------------
-- Table structure for recipe
-- ----------------------------
DROP TABLE IF EXISTS `recipe`;
CREATE TABLE `recipe` (
  `recipeid` int(11) NOT NULL AUTO_INCREMENT,
  `device_group_did` int(11) NOT NULL DEFAULT '0' COMMENT '设备资产ID',
  `creater` varchar(50) NOT NULL DEFAULT '0' COMMENT '创建者',
  `updatetime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  `enabled` int(11) NOT NULL DEFAULT '1' COMMENT '1:使用；0:不使用',
  PRIMARY KEY (`recipeid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='配方管理';

-- ----------------------------
-- Table structure for recipehistory
-- ----------------------------
DROP TABLE IF EXISTS `recipehistory`;
CREATE TABLE `recipehistory` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='配方信息记录历史';

-- ----------------------------
-- Table structure for recipe_fields
-- ----------------------------
DROP TABLE IF EXISTS `recipe_fields`;
CREATE TABLE `recipe_fields` (
  `recipeid` int(11) NOT NULL AUTO_INCREMENT,
  `FieldName` varchar(50) NOT NULL COMMENT '字段名称',
  `FieldDescription` varchar(50) NOT NULL COMMENT '字段描述',
  `registeraddress` varchar(50) DEFAULT NULL COMMENT '32位PLC寄存器地址（比如D0）',
  `plcaddress` varchar(50) DEFAULT NULL COMMENT 'PLC地址',
  `createTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  PRIMARY KEY (`recipeid`),
  UNIQUE KEY `FieldName` (`FieldName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='配方字段';

-- ----------------------------
-- Table structure for roles
-- ----------------------------
DROP TABLE IF EXISTS `roles`;
CREATE TABLE `roles` (
  `roleId` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `roleName` varchar(50) NOT NULL COMMENT '角色名称',
  `permissionCodes` varchar(10000) DEFAULT NULL COMMENT '菜单权限编码',
  `createTime` datetime NOT NULL COMMENT '创建时间',
  `remark` varchar(250) DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`roleId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for solution
-- ----------------------------
DROP TABLE IF EXISTS `solution`;
CREATE TABLE `solution` (
  `did` int(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `content` varchar(255) NOT NULL COMMENT '方案内容',
  PRIMARY KEY (`did`)
) ENGINE=InnoDB AUTO_INCREMENT=135 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for solution_image
-- ----------------------------
DROP TABLE IF EXISTS `solution_image`;
CREATE TABLE `solution_image` (
  `did` int(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `path` varchar(255) NOT NULL COMMENT '图片路径',
  PRIMARY KEY (`did`)
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for unit
-- ----------------------------
DROP TABLE IF EXISTS `unit`;
CREATE TABLE `unit` (
  `unit_did` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `unit_no` varchar(50) NOT NULL COMMENT '逻辑编号',
  `unit_name` varchar(50) NOT NULL COMMENT '部件名称',
  PRIMARY KEY (`unit_did`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users` (
  `userId` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `userName` varchar(50) NOT NULL COMMENT '帐号',
  `password` varchar(255) NOT NULL COMMENT '密码',
  `name` varchar(50) NOT NULL COMMENT '姓名',
  `roleId` int(11) NOT NULL COMMENT '角色编号',
  `createTime` datetime NOT NULL COMMENT '创建时间',
  `gender` int(11) DEFAULT NULL COMMENT '性别',
  `LastLoginTime` datetime DEFAULT NULL COMMENT '最后登录时间',
  `craft_dids` varchar(500) DEFAULT NULL COMMENT '工艺编号',
  PRIMARY KEY (`userId`),
  KEY `fk_user_roles_roleId` (`roleId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for usersinfo
-- ----------------------------
DROP TABLE IF EXISTS `usersinfo`;
CREATE TABLE `usersinfo` (
  `craftwork` varchar(255) DEFAULT NULL,
  `process` varchar(255) DEFAULT NULL,
  `quarters` varchar(255) DEFAULT NULL,
  `segments` varchar(255) DEFAULT NULL,
  `staffid` varchar(255) DEFAULT NULL,
  `time` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for vulnerable
-- ----------------------------
DROP TABLE IF EXISTS `vulnerable`;
CREATE TABLE `vulnerable` (
  `Iden` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `Name` varchar(50) DEFAULT NULL COMMENT '名称',
  `Used` int(11) DEFAULT NULL COMMENT '已用次数',
  `Expect` int(11) DEFAULT NULL COMMENT '预期次数',
  `Exchange` int(11) DEFAULT NULL COMMENT '更换次数',
  `time` datetime DEFAULT NULL COMMENT '时间',
  `User` varchar(50) DEFAULT NULL COMMENT '上次操作员工',
  `PicNum1` varchar(50) DEFAULT NULL COMMENT '图号',
  `PicNum2` varchar(50) DEFAULT NULL COMMENT '图号',
  PRIMARY KEY (`Iden`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for workcalendar
-- ----------------------------
DROP TABLE IF EXISTS `workcalendar`;
CREATE TABLE `workcalendar` (
  `did` int(11) NOT NULL AUTO_INCREMENT,
  `device_group_did` int(11) NOT NULL COMMENT '设备ID',
  `workTime` int(11) NOT NULL COMMENT '当天工作时间(H)',
  `date` date NOT NULL COMMENT '日期',
  `updatetime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`did`),
  UNIQUE KEY `key_unique` (`device_group_did`,`date`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8 COMMENT='工作日历';
