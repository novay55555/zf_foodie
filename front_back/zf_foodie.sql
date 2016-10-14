/*
Navicat MySQL Data Transfer

Source Server         : 1234
Source Server Version : 50633
Source Host           : localhost:3306
Source Database       : zf_foodie

Target Server Type    : MYSQL
Target Server Version : 50633
File Encoding         : 65001

Date: 2016-10-13 10:16:03
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for eat_food_category
-- ----------------------------
DROP TABLE IF EXISTS `eat_food_category`;
CREATE TABLE `eat_food_category` (
  `pk_id` varchar(40) NOT NULL DEFAULT '',
  `category_name` varchar(40) DEFAULT NULL,
  `category_code` varchar(40) DEFAULT NULL,
  `parent_pkid` varchar(40) DEFAULT NULL,
  `remark` varchar(4000) DEFAULT NULL,
  `flag_status` decimal(1,0) DEFAULT NULL,
  `flag_sort` decimal(10,0) DEFAULT NULL,
  `user_pkid` varchar(40) DEFAULT NULL,
  `make_emp` varchar(40) DEFAULT NULL,
  `make_date` datetime DEFAULT NULL,
  `modify_emp` varchar(40) DEFAULT NULL,
  `modify_date` datetime DEFAULT NULL,
  PRIMARY KEY (`pk_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of eat_food_category
-- ----------------------------
INSERT INTO `eat_food_category` VALUES ('2A9EB532-8010-49BB-B729-A1D6E9557699', '广东小吃', null, null, null, '0', null, null, null, null, null, null);
INSERT INTO `eat_food_category` VALUES ('N82E71E48-F478-4C47-96B2-DD2AFC0D9311', '北京小吃', null, null, null, '0', null, null, null, null, null, null);

-- ----------------------------
-- Table structure for eat_food_list
-- ----------------------------
DROP TABLE IF EXISTS `eat_food_list`;
CREATE TABLE `eat_food_list` (
  `pk_id` varchar(40) NOT NULL,
  `food_category` varchar(40) DEFAULT NULL,
  `food_code` varchar(40) DEFAULT NULL,
  `food_name` varchar(40) DEFAULT NULL,
  `short_name` varchar(40) DEFAULT NULL,
  `food_name_eng` varchar(40) DEFAULT NULL,
  `food_price` decimal(16,6) DEFAULT NULL,
  `user_pkid` varchar(40) DEFAULT NULL,
  `remark` varchar(4000) DEFAULT NULL,
  `flag_status` decimal(1,0) DEFAULT NULL,
  `flag_sort` decimal(10,0) DEFAULT NULL,
  `make_emp` varchar(40) DEFAULT NULL,
  `make_date` datetime DEFAULT NULL,
  `modify_emp` varchar(40) DEFAULT NULL,
  `modify_date` datetime DEFAULT NULL,
  PRIMARY KEY (`pk_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of eat_food_list
-- ----------------------------
INSERT INTO `eat_food_list` VALUES ('NC05254E2-1A5F-4B57-8CF6-F2339DEC1BA6', '2A9EB532-8010-49BB-B729-A1D6E9557699', null, '广东2', null, null, null, null, null, '0', null, null, null, null, null);
INSERT INTO `eat_food_list` VALUES ('ND1D03D89-8687-4FD8-A46C-9AC23B1FF5C2', '2A9EB532-8010-49BB-B729-A1D6E9557699', null, '广东1', null, null, null, null, null, '0', null, null, null, null, null);
INSERT INTO `eat_food_list` VALUES ('NE4F4A286-AD2F-4B92-8EF5-2F83CAC7F86B', 'N82E71E48-F478-4C47-96B2-DD2AFC0D9311', null, '北京1', null, null, null, null, null, null, null, null, null, null, null);

-- ----------------------------
-- Table structure for eat_system_user_info
-- ----------------------------
DROP TABLE IF EXISTS `eat_system_user_info`;
CREATE TABLE `eat_system_user_info` (
  `pk_id` varchar(40) NOT NULL,
  `user_password` varchar(40) DEFAULT NULL,
  `user_nick_name` varchar(40) DEFAULT NULL,
  `remark` varchar(4000) DEFAULT NULL,
  `flag_status` decimal(1,0) DEFAULT NULL,
  `flag_sort` decimal(10,0) DEFAULT NULL,
  `make_emp` varchar(40) DEFAULT NULL,
  `make_date` datetime DEFAULT NULL,
  `modify_emp` varchar(40) DEFAULT NULL,
  `modify_date` datetime DEFAULT NULL,
  `login_state` varchar(40) DEFAULT NULL,
  `flag_power` decimal(1,0) DEFAULT NULL,
  PRIMARY KEY (`pk_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of eat_system_user_info
-- ----------------------------
INSERT INTO `eat_system_user_info` VALUES ('admin', '1234', null, null, '0', null, null, null, null, null, null, null);
