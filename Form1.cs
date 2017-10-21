using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ONE
{
    public partial class Form1 : Form
    {
        
        string MacList;
        string EntryListf;
        string ccdhcp = null;
        string plkjmac;
        string[] num = new string[] {"01","02","03","04","05","06","07","08","09","10","12","13","14","15","16","17","18","19","20","21","22","23","25","26","27","28","29","34","36","37","38", "39","40","41","42","33","32"};//Ϊ8M��20M��ͥ�ײ���ƣ���jfname[]��һһ��Ӧ��ϵ�����������챦
        string[] num2 = new string[] {"01","02","03","04","05","06","07","08","09","10","12","13","14","15","16","17","18","19","20","21","22","23","25","26","27","28","29","34","36","37","38", "39","40","41","42","33","32","73","74","75","77","79","81","82","84","88","89","91","92"};//Ϊ�챦��ƣ���jfname2[]��һһ��Ӧ��ϵ��������¡,73ǰ����������������Ч
		string[] jfname = new string[] { "�����", "��ɽ", "�ʸ�", "��԰", "÷��", "������", "�ٻ�", "������", "����", "����",  "ɳͷ��", "��ó", "��Դ�壨���ƴ壩", "���ȳ�", "����", "����", "����", "�Ƽ�԰", "����", "����", "����", "��ڱ�", "������", "԰��԰", "����", "��ó��", "����", "������", "���㶫", "�ϲ���", "����", "������", "�����", "����", "��ƺ��", "����", "����", "�챦21����73�Ż�����", "�챦42����74�Ż�����", "�챦����ί��75��", "�챦��ͨ�ǣ�77��", "�챦������79��", "�챦ɳ����81��", "�챦���ˣ�82��", "�챦�ɸڣ�84��", "�챦ˮ�88��", "�챦������89��", "�챦���Σ�91��", "�챦������92��", "��¡���ģ�100��", "��¡���ڣ�104��", "��¡��ڣ�105��", "��¡������111��", "��¡������103��", "��¡ƽ����106��", "��¡ƺ�أ�107��", "��¡ƺɽ��108��" };
        string[] jfdhcp = new string[] { "107", "113", "101", "111", "111", "105", "113", "103", "101", "105",  "107", "103", "109", "113", "107", "107", "109", "111", "107", "107", "107", "107", "101", "103", "109", "101", "105", "103", "105", "109", "111", "109", "105", "103", "107", "109", "109", "87", "87", "87", "87", "87", "87", "87", "87", "87", "87", "87", "87", "91", "91", "91", "91", "91", "91", "91", "91" };
        //string[] jfnormal = new string[] { "CMTS_01_CM_normal", "CMTS_02_01_CM_normal", "CMTS_03_01_CM_normal", "CMTS_04_01_CM_normal", "CMTS_05_01_CM_normal", "CMTS_06_01_CM_normal", "CMTS_07_01_CM_normal", "CMTS_08_01_CM_normal", "CMTS_09_01_CM_normal", "CMTS_10_01_CM_normal", "CMTS_11_01_CM_normal", "CMTS_12_CM_normal", "CMTS_13_01_CM_normal", "CMTS_14_01_CM_normal", "CMTS_15_01_CM_normal", "CMTS_16_CM_normal", "CMTS_17_CM_normal", "CMTS_18_01_CM_normal", "CMTS_19_01_CM_normal", "CMTS_20_CM_normal", "CMTS_21_CM_normal", "uBR7111E_22_CM_normal", "uBR7223_23_CM_normal", "CMTS_25_01_CM_normal", "CMTS_26_01_CM_normal", "CMTS_27_01_CM_normal", "CMTS_28_01_CM_normal", "CMTS_29_01_CM_normal", "CMTS_34_01_CM_normal", "CMTS_36_01_CM_normal", "CMTS_37_01_CM_normal", "CMTS_38_01_CM_normal", "CMTS_39_01_CM_normal", "CMTS_40_01_CM_normal", "CMTS_41_01_CM_normal", "CMTS_42_01_CM_normal", "CMTS_33_01_CM_normal", "CMTS_32_01_CM_normal", "CMTS_73_01_CM_normal", "CMTS_74_01_CM_normal", "CMTS_75_01_CM_normal", "CMTS_77_01_CM_normal", "CMTS_79_01_CM_normal", "CMTS_81_01_CM_normal", "CMTS_82_01_CM_normal", "CMTS_84_01_CM_normal", "CMTS_88_01_CM_normal", "CMTS_89_01_CM_normal", "CMTS_91_01_CM_normal", "CMTS_92_01_CM_normal", "CMTS_100_01_CM_normal", "CMTS_104_01_CM_normal", "CMTS_105_01_CM_normal", "CMTS_111_01_CM_normal", "CMTS_103_01_CM_normal", "CMTS_106_01_CM_normal", "CMTS_107_01_CM_normal", "CMTS_108_01_CM_normal" };
        string[] jfglobe = new string[] { "CMTS_01_CM_global", "CMTS_02_01_CM_global", "CMTS_03_01_CM_global", "CMTS_04_01_CM_global", "CMTS_05_01_CM_global", "CMTS_06_01_CM_global", "CMTS_07_01_CM_global", "CMTS_08_01_CM_global", "CMTS_09_01_CM_global", "CMTS_10_01_CM_global",  "CMTS_12_CM_global", "CMTS_13_01_CM_global", "CMTS_14_01_CM_global", "CMTS_15_01_CM_global", "CMTS_16_CM_global", "CMTS_17_CM_global", "CMTS_18_01_CM_global", "CMTS_19_01_CM_global", "CMTS_20_CM_global", "CMTS_21_CM_global", "CMTS_22_CM_global", "CMTS_23_CM_global", "CMTS_25_01_CM_global", "CMTS_26_01_CM_global", "CMTS_27_01_CM_global", "CMTS_28_01_CM_global", "CMTS_29_01_CM_global", "CMTS_34_01_CM_global", "CMTS_36_01_CM_global", "CMTS_37_01_CM_global", "CMTS_38_01_CM_global", "CMTS_39_CM_global", "CMTS_40_01_CM_global", "CMTS_41_01_CM_global", "CMTS_42_01_CM_global",  "CMTS_33_01_CM_global", "CMTS_32_01_CM_global", "CMTS_73_01_CM_global", "CMTS_74_01_CM_global", "CMTS_75_01_CM_global", "CMTS_77_01_CM_global", "CMTS_79_01_CM_global", "CMTS_81_01_CM_global", "CMTS_82_01_CM_global", "CMTS_84_01_CM_global", "CMTS_88_01_CM_normal", "CMTS_89_01_CM_global", "CMTS_91_01_CM_global", "CMTS_92_01_CM_global", "CMTS_100_01_CM_global", "CMTS_104_01_CM_global", "CMTS_105_01_CM_global", "CMTS_111_01_CM_global", "CMTS_103_01_CM_global", "CMTS_106_01_CM_global", "CMTS_107_01_CM_global", "CMTS_108_01_CM_global" };
        string[] jfprivate = new string[] { "CMTS_01_CM_private", "CMTS_02_01_CM_private", "CMTS_03_01_CM_private", "CMTS_04_01_CM_private", "CMTS_05_01_CM_private", "CMTS_06_01_CM_private", "CMTS_07_01_CM_private", "CMTS_08_01_CM_private", "CMTS_09_01_CM_private", "CMTS_10_01_CM_private",  "CMTS_12_CM_private", "CMTS_13_01_CM_private", "CMTS_14_01_CM_private", "CMTS_15_01_CM_private", "CMTS_16_CM_private", "CMTS_17_CM_private", "CMTS_18_01_CM_private", "CMTS_19_01_CM_private", "CMTS_20_CM_private", "CMTS_21_CM_private", "CMTS_22_CM_private", "CMTS_23_CM_private", "CMTS_25_01_CM_private", "CMTS_26_01_CM_private", "CMTS_27_01_CM_private", "CMTS_28_01_CM_private", "CMTS_29_01_CM_private", "CMTS_34_01_CM_private", "CMTS_36_01_CM_private", "CMTS_37_01_CM_private", "CMTS_38_01_CM_private", "CMTS_39_CM_private", "CMTS_40_CM_private", "CMTS_41_CM_private", "CMTS_42_01_CM_private","CMTS_33_01_CM_private", "CMTS_32_01_CM_private", "CMTS_73_01_CM_private", "CMTS_74_01_CM_private", "CMTS_75_01_CM_private", "CMTS_77_01_CM_private", "CMTS_79_01_CM_private", "CMTS_81_01_CM_private", "CMTS_82_01_CM_private", "CMTS_84_01_CM_private", "CMTS_88_01_CM_private", "CMTS_89_01_CM_private", "CMTS_91_01_CM_private", "CMTS_92_01_CM_private", "CMTS_100_01_CM_private", "CMTS_104_01_CM_private", "CMTS_105_01_CM_private", "CMTS_111_01_CM_private", "CMTS_103_01_CM_private", "CMTS_106_01_CM_private", "CMTS_107_01_CM_private", "CMTS_108_01_CM_private" };
        string[] jfistbxdk = new string[] { "istb_XDK_01_CPE_NAT", "istb_XDK_02_CPE_NAT", "istb_XDK_03_CPE_NAT", "istb_XDK_04_CPE_NAT", "istb_XDK_05_CPE_NAT", "istb_XDK_06_CPE_NAT", "istb_XDK_07_CPE_NAT", "istb_XDK_08_CPE_NAT", "istb_XDK_09_CPE_NAT", "istb_XDK_10_CPE_NAT", "istb_XDK_12_CPE_NAT", "istb_XDK_13_CPE_NAT", "istb_XDK_14_CPE_NAT", "istb_XDK_15_CPE_NAT", "istb_XDK_16_CPE_NAT", "istb_XDK_17_CPE_NAT", "istb_XDK_18_CPE_NAT", "istb_XDK_19_CPE_NAT", "istb_XDK_20_CPE_NAT", "istb_XDK_21_CPE_NAT", "istb_XDK_22_CPE_NAT", "istb_XDK_23_CPE_NAT", "istb_XDK_25_CPE_NAT", "istb_XDK_26_CPE_NAT", "istb_XDK_27_CPE_NAT", "istb_XDK_28_CPE_NAT", "istb_XDK_29_CPE_NAT", "istb_XDK_34_CPE_NAT", "istb_XDK_36_CPE_NAT", "istb_XDK_37_CPE_NAT", "istb_XDK_38_CPE_NAT", "istb_XDK_39_CPE_NAT", "istb_XDK_40_CPE_NAT", "istb_XDK_41_CPE_NAT", "istb_XDK_42_CPE_NAT", "istb_XDK_33_CPE_NAT", "istb_XDK_32_CPE_NAT", "istb_XDK_73_CPE_NAT", "istb_XDK_74_CPE_NAT", "istb_XDK_75_CPE_NAT", "istb_XDK_77_CPE_NAT", "istb_XDK_79_CPE_NAT", "istb_XDK_81_CPE_NAT", "istb_XDK_82_CPE_NAT", "istb_XDK_84_CPE_NAT", "istb_XDK_88_CPE_NAT", "istb_XDK_89_CPE_NAT", "istb_XDK_91_CPE_NAT", "istb_XDK_92_CPE_NAT", "istb_XDK_100_CPE_NAT", "istb_XDK_104_CPE_NAT", "istb_XDK_105_CPE_NAT", "istb_XDK_111_CPE_NAT", "istb_XDK_103_XDK_changyou_XDK_CPE_XDK_Global", "istb_XDK_106_CPE_NAT", "istb_XDK_107_XDK_changyou_XDK_CPE_XDK_Global", "istb_XDK_108_CPE_NAT" };
        //string[] jfistb1m = new string[] { "istb_01_1M_CPE_Global", "istb_02_1M_CPE_Global", "istb_03_1M_CPE_Global", "istb_04_1M_CPE_Global", "istb_05_1M_CPE_Global", "istb_06_1M_CPE_Global", "istb_07_1M_CPE_Global", "istb_08_1M_CPE_Global", "istb_09_1M_CPE_Global", "istb_10_1M_CPE_Global", "istb_11_1M_CPE_Global", "istb_12_1M_CPE_Global", "istb_13_1M_CPE_Global", "istb_14_1M_CPE_Global", "istb_15_1M_CPE_Global", "istb_16_1M_CPE_Global", "istb_17_1M_CPE_Global", "istb_18_1M_CPE_Global", "istb_19_1M_CPE_Global", "istb_20_1M_CPE_Global", "istb_21_1M_CPE_Global", "istb_22_1M_CPE_Global", "istb_23_1M_CPE_Global", "istb_25_1M_CPE_Global", "istb_26_1M_CPE_Global", "istb_27_1M_CPE_Global", "istb_28_1M_CPE_Global", "istb_29_1M_CPE_Global", "istb_34_1M_CPE_Global", "istb_36_1M_CPE_Global", "istb_37_1M_CPE_Global", "istb_38_1M_CPE_Global", "istb_39_1M_CPE_Global", "istb_40_1M_CPE_Global", "istb_41_1M_CPE_Global", "istb_42_dazhong_CPE_Global", "istb_33_1M_CPE_Global", "istb_32_1M_CPE_Global", "istb_73_1M_CPE_Global", "istb_74_1M_CPE_Global", "istb_75_1M_CPE_Global", "istb_77_1M_CPE_Global", "istb_79_1M_CPE_Global", "istb_81_1M_CPE_Global", "istb_82_1M_CPE_Global", "istb_84_1M_CPE_Global", "istb_88_1M_CPE_Global", "istb_89_1M_CPE_Global", "istb_91_1M_CPE_Global", "istb_92_1M_CPE_Global", "istb_100_1M_CPE_Global", "istb_104_1M_CPE_Global", "istb_105_1M_CPE_Global", "istb_111_1M_CPE_Global", "istb_103_dazhong_CPE_Global", "istb_106_1M_CPE_Global", "istb_107_dazhong_CPE_Global", "istb_108_1M_CPE_Global" };
        string[] jfistb2m = new string[] { "istb_01_2M_CPE_Global", "istb_02_2M_CPE_Global", "istb_03_2M_CPE_Global", "istb_04_2M_CPE_Global", "istb_05_2M_CPE_Global", "istb_06_2M_CPE_Global", "istb_07_2M_CPE_Global", "istb_08_2M_CPE_Global", "istb_09_2M_CPE_Global", "istb_10_2M_CPE_Global",  "istb_12_2M_CPE_Global", "istb_13_2M_CPE_Global", "istb_14_2M_CPE_Global", "istb_15_2M_CPE_Global", "istb_16_2M_CPE_Global", "istb_17_2M_CPE_Global", "istb_18_2M_CPE_Global", "istb_19_2M_CPE_Global", "istb_20_2M_CPE_Global", "istb_21_2M_CPE_Global", "istb_22_2M_CPE_Global", "istb_23_2M_CPE_Global", "istb_25_2M_CPE_Global", "istb_26_2M_CPE_Global", "istb_27_2M_CPE_Global", "istb_28_2M_CPE_Global", "istb_29_2M_CPE_Global", "istb_34_2M_CPE_Global", "istb_36_2M_CPE_Global", "istb_37_2M_CPE_Global", "istb_38_2M_CPE_Global", "istb_39_2M_CPE_Global", "istb_40_2M_CPE_Global", "istb_41_2M_CPE_Global", "istb_42_jingdian_CPE_Global","istb_32_2M_CPE_Global", "istb_33_2M_CPE_Global", "istb_73_2M_CPE_Global", "istb_74_2M_CPE_Global", "istb_75_2M_CPE_Global", "istb_77_2M_CPE_Global", "istb_79_2M_CPE_Global", "istb_81_2M_CPE_Global", "istb_82_2M_CPE_Global", "istb_84_2M_CPE_Global", "istb_88_2M_CPE_Global", "istb_89_2M_CPE_Global", "istb_91_2M_CPE_Global", "istb_92_2M_CPE_Global", "istb_100_2M_CPE_Global", "istb_104_2M_CPE_Global", "istb_105_2M_CPE_Global", "istb_111_2M_CPE_Global", "istb_103_jingdian_CPE_Global", "istb_106_2M_CPE_Global", "istb_107_jingdian_CPE_Global", "istb_108_2M_CPE_Global" };
        //string[] jfistb4m = new string[] { "istb_01_4M_CPE_Global", "istb_02_4M_CPE_Global", "istb_03_4M_CPE_Global", "istb_04_4M_CPE_Global", "istb_05_4M_CPE_Global", "istb_06_4M_CPE_Global", "istb_07_4M_CPE_Global", "istb_08_4M_CPE_Global", "istb_09_4M_CPE_Global", "istb_10_4M_CPE_Global", "istb_11_4M_CPE_Global", "istb_12_4M_CPE_Global", "istb_13_4M_CPE_Global", "istb_14_4M_CPE_Global", "istb_15_4M_CPE_Global", "istb_16_4M_CPE_Global", "istb_17_4M_CPE_Global", "istb_18_4M_CPE_Global", "istb_19_4M_CPE_Global", "istb_20_4M_CPE_Global", "istb_21_4M_CPE_Global", "istb_22_4M_CPE_Global", "istb_23_4M_CPE_Global", "istb_25_4M_CPE_Global", "istb_26_4M_CPE_Global", "istb_27_4M_CPE_Global", "istb_28_4M_CPE_Global", "istb_29_4M_CPE_Global", "istb_34_4M_CPE_Global", "istb_36_4M_CPE_Global", "istb_37_4M_CPE_Global", "istb_38_4M_CPE_Global", "istb_39_4M_CPE_Global", "istb_40_4M_CPE_Global", "istb_41_4M_CPE_Global", "istb_42_jingying_CPE_Global", "istb_33_4M_CPE_Global", "istb_32_4M_CPE_Global", "istb_73_4M_CPE_Global", "istb_74_4M_CPE_Global", "istb_75_4M_CPE_Global", "istb_77_4M_CPE_Global", "istb_79_4M_CPE_Global", "istb_81_4M_CPE_Global", "istb_82_4M_CPE_Global", "istb_84_4M_CPE_Global", "istb_88_4M_CPE_Global", "istb_89_4M_CPE_Global", "istb_91_4M_CPE_Global", "istb_92_4M_CPE_Global", "istb_100_4M_CPE_Global", "istb_104_4M_CPE_Global", "istb_105_4M_CPE_Global", "istb_111_4M_CPE_Global", "istb_103_jingying_CPE_Global", "istb_106_4M_CPE_Global", "istb_107_jingying_CPE_Global", "istb_108_4M_CPE_Global" };
        //string[] jfistb8m = new string[] { "istb_01_8M_CPE_Global", "istb_02_8M_CPE_Global", "istb_03_8M_CPE_Global", "istb_04_8M_CPE_Global", "istb_05_8M_CPE_Global", "istb_06_8M_CPE_Global", "istb_07_8M_CPE_Global", "istb_08_8M_CPE_Global", "istb_09_8M_CPE_Global", "istb_10_8M_CPE_Global", "istb_11_8M_CPE_Global", "istb_12_8M_CPE_Global", "istb_13_8M_CPE_Global", "istb_14_8M_CPE_Global", "istb_15_8M_CPE_Global", "istb_16_8M_CPE_Global", "istb_17_8M_CPE_Global", "istb_18_8M_CPE_Global", "istb_19_8M_CPE_Global", "istb_20_8M_CPE_Global", "istb_21_8M_CPE_Global", "istb_22_8M_CPE_Global", "istb_23_8M_CPE_Global", "istb_25_8M_CPE_Global", "istb_26_8M_CPE_Global", "istb_27_8M_CPE_Global", "istb_28_8M_CPE_Global", "istb_29_8M_CPE_Global", "istb_34_8M_CPE_Global", "istb_36_8M_CPE_Global", "istb_37_8M_CPE_Global", "istb_38_8M_CPE_Global", "istb_39_8M_CPE_Global", "istb_40_8M_CPE_Global", "istb_41_8M_CPE_Global", "istb_42_changyou_CPE_Global", "istb_33_8M_CPE_Global", "istb_32_8M_CPE_Global", "istb_73_8M_CPE_Global", "istb_74_8M_CPE_Global", "istb_75_8M_CPE_Global", "istb_77_8M_CPE_Global", "istb_79_8M_CPE_Global", "istb_81_8M_CPE_Global", "istb_82_8M_CPE_Global", "istb_84_8M_CPE_Global", "istb_88_8M_CPE_Global", "istb_89_8M_CPE_Global", "istb_91_8M_CPE_Global", "istb_92_8M_CPE_Global", "istb_100_8M_CPE_Global", "istb_104_8M_CPE_Global", "istb_105_8M_CPE_Global", "istb_111_8M_CPE_Global", "istb_103_changyou_CPE_Global", "istb_106_8M_CPE_Global", "istb_107_changyou_CPE_Global", "istb_108_8M_CPE_Global" };
        //string[] jfistb30m = new string[] { "istb_01_30M_CPE_Global", "istb_02_30M_CPE_Global", "istb_03_30M_CPE_Global", "istb_04_30M_CPE_Global", "istb_05_30M_CPE_Global", "istb_06_30M_CPE_Global", "istb_07_30M_CPE_Global", "istb_08_30M_CPE_Global", "istb_09_30M_CPE_Global", "istb_10_30M_CPE_Global", "istb_11_30M_CPE_Global", "istb_12_30M_CPE_Global", "istb_13_30M_CPE_Global", "istb_14_30M_CPE_Global", "istb_15_30M_CPE_Global", "istb_16_30M_CPE_Global", "istb_17_30M_CPE_Global", "istb_18_30M_CPE_Global", "istb_19_30M_CPE_Global", "istb_20_30M_CPE_Global", "istb_21_30M_CPE_Global", "istb_22_30M_CPE_Global", "istb_23_30M_CPE_Global", "istb_25_30M_CPE_Global", "istb_26_30M_CPE_Global", "istb_27_30M_CPE_Global", "istb_28_30M_CPE_Global", "istb_29_30M_CPE_Global", "istb_29_30M_CPE_Global", "istb_34_30M_CPE_Global", "istb_37_30M_CPE_Global", "istb_38_30M_CPE_Global", "istb_39_30M_CPE_Global", "istb_40_30M_CPE_Global", "istb_41_30M_CPE_Global", "istb_42_zhuoyue_CPE_Global", "istb_33_30M_CPE_Global", "istb_32_30M_CPE_Global", "istb_73_30M_CPE_Global", "istb_74_30M_CPE_Global", "istb_75_30M_CPE_Global", "istb_77_30M_CPE_Global", "istb_79_30M_CPE_Global", "istb_81_30M_CPE_Global", "istb_82_30M_CPE_Global", "istb_84_30M_CPE_Global", "istb_88_30M_CPE_Global", "istb_89_30M_CPE_Global", "istb_91_30M_CPE_Global", "istb_92_30M_CPE_Global", "istb_100_30M_CPE_Global", "istb_104_30M_CPE_Global", "istb_105_30M_CPE_Global", "istb_111_30M_CPE_Global", "istb_103_zhuoyue_CPE_Global", "istb_106_30M_CPE_Global", "istb_107_zhuoyue_CPE_Global", "istb_108_30M_CPE_Global" };
        //string[] jfjingying = new string[] { "CMTS_01_CM_jingying", "CMTS_02_CM_jingying", "CMTS_03_CM_jingying", "CMTS_04_CM_jingying", "CMTS_05_CM_jingying", "CMTS_06_CM_jingying", "CMTS_07_CM_jingying", "CMTS_08_CM_jingying", "CMTS_09_CM_jingying", "CMTS_10_CM_jingying", "CMTS_11_CM_jingying", "CMTS_12_CM_jingying", "CMTS_13_CM_jingying", "CMTS_14_CM_jingying", "CMTS_15_CM_jingying", "CMTS_16_CM_jingying", "CMTS_17_CM_jingying", "CMTS_18_CM_jingying", "CMTS_19_CM_jingying", "CMTS_20_CM_jingying", "CMTS_21_CM_jingying", "CMTS_22_CM_jingying", "CMTS_23_CM_jingying", "CMTS_25_CM_jingying", "CMTS_26_CM_jingying", "CMTS_27_CM_jingying", "CMTS_28_CM_jingying", "CMTS_29_CM_jingying", "CMTS_34_CM_jingying", "CMTS_36_CM_jingying", "CMTS_37_CM_jingying", "CMTS_38_CM_jingying", "CMTS_39_CM_jingying", "CMTS_40_CM_jingying", "CMTS_41_CM_jingying", "CMTS_42_CM_jingying", "CMTS_33_CM_jingying", "CMTS_32_CM_jingying", "CMTS_73_CM_jingying", "CMTS_74_CM_jingying", "CMTS_75_CM_jingying", "CMTS_77_CM_jingying", "CMTS_79_CM_jingying", "CMTS_81_CM_jingying", "CMTS_82_CM_jingying", "CMTS_84_CM_jingying", "CMTS_88_CM_jingying", "CMTS_89_CM_jingying", "CMTS_91_CM_jingying", "CMTS_92_CM_jingying", "CMTS_100_CM_jingying", "CMTS_104_CM_jingying", "CMTS_105_CM_jingying", "CMTS_111_CM_jingying", "CMTS_103_CM_jingying", "CMTS_106_CM_jingying", "CMTS_107_CM_jingying", "CMTS_108_CM_jingying" };
        string[] jfjingdian = new string[] { "CMTS_01_CM_jingdian", "CMTS_02_CM_jingdian", "CMTS_03_CM_jingdian", "CMTS_04_CM_jingdian", "CMTS_05_CM_jingdian", "CMTS_06_CM_jingdian", "CMTS_07_CM_jingdian", "CMTS_08_CM_jingdian", "CMTS_09_CM_jingdian", "CMTS_10_CM_jingdian", "CMTS_12_CM_jingdian", "CMTS_13_CM_jingdian", "CMTS_14_CM_jingdian", "CMTS_15_CM_jingdian", "CMTS_16_CM_jingdian", "CMTS_17_CM_jingdian", "CMTS_18_CM_jingdian", "CMTS_19_CM_jingdian", "CMTS_20_CM_jingdian", "CMTS_21_CM_jingdian", "CMTS_22_CM_jingdian", "CMTS_23_CM_jingdian", "CMTS_25_CM_jingdian", "CMTS_26_CM_jingdian", "CMTS_27_CM_jingdian", "CMTS_28_CM_jingdian", "CMTS_29_CM_jingdian", "CMTS_34_CM_jingdian", "CMTS_36_CM_jingdian", "CMTS_37_CM_jingdian", "CMTS_38_CM_jingdian", "CMTS_39_CM_jingdian", "CMTS_40_CM_jingdian", "CMTS_41_CM_jingdian", "CMTS_42_CM_jingdian", "CMTS_33_CM_jingdian", "CMTS_32_CM_jingdian", "CMTS_73_CM_jingdian", "CMTS_74_CM_jingdian", "CMTS_75_CM_jingdian", "CMTS_77_CM_jingdian", "CMTS_79_CM_jingdian", "CMTS_81_CM_jingying", "CMTS_82_CM_jingying", "CMTS_84_CM_jingdian", "CMTS_88_CM_jingdian", "CMTS_89_CM_jingdian", "CMTS_91_CM_jingdian", "CMTS_92_CM_jingdian", "CMTS_100_CM_jingdian", "CMTS_104_CM_jingdian", "CMTS_105_CM_jingdian", "CMTS_111_CM_jingdian", "CMTS_103_CM_jingdian", "CMTS_106_CM_jingdian", "CMTS_107_CM_jingdian", "CMTS_108_CM_jingdian" };
        //string[] jf1M = new string[] { "CMTS_01_CM_1M", "CMTS_02_CM_1M", "CMTS_03_CM_1M", "CMTS_04_CM_1M", "CMTS_05_CM_1M", "CMTS_06_CM_1M", "CMTS_07_CM_1M", "CMTS_08_CM_1M", "CMTS_09_CM_1M", "CMTS_10_CM_1M", "CMTS_11_CM_1M", "CMTS_12_CM_1M", "CMTS_13_CM_1M", "CMTS_14_CM_1M", "CMTS_15_CM_1M", "CMTS_16_CM_1M", "CMTS_17_CM_1M", "CMTS_18_CM_1M", "CMTS_19_CM_1M", "CMTS_20_CM_1M", "CMTS_21_CM_1M", "CMTS_22_CM_1M", "CMTS_23_CM_1M", "CMTS_25_CM_1M", "CMTS_26_CM_1M", "CMTS_27_CM_1M", "CMTS_28_CM_1M", "CMTS_29_CM_1M", "CMTS_34_CM_1M", "CMTS_36_CM_1M", "CMTS_37_CM_1M", "CMTS_38_CM_1M", "CMTS_39_CM_1M", "CMTS_40_CM_1M", "CMTS_41_CM_1M", "CMTS_42_CM_dazhong", "CMTS_33_CM_1M", "CMTS_32_CM_1M", "CMTS_73_CM_dazhong", "CMTS_74_CM_dazhong", "CMTS_75_CM_dazhong", "CMTS_77_CM_dazhong", "CMTS_79_CM_dazhong", "CMTS_81_CM_dazhong", "CMTS_82_CM_dazhong", "CMTS_84_CM_dazhong", "CMTS_88_CM_dazhong", "CMTS_89_CM_dazhong", "CMTS_91_CM_dazhong", "CMTS_92_CM_dazhong", "CMTS_100_CM_dazhong", "CMTS_104_CM_dazhong", "CMTS_105_CM_dazhong", "CMTS_111_CM_dazhong", "CMTS_103_CM_dazhong", "CMTS_106_CM_dazhong", "CMTS_107_CM_dazhong", "CMTS_108_CM_dazhong" };
        //string[] jf3M = new string[] { "CMTS_01_CM_3M", "CMTS_02_CM_3M", "CMTS_03_CM_3M", "CMTS_04_CM_3M", "CMTS_05_CM_3M", "CMTS_06_CM_3M", "CMTS_07_CM_3M", "CMTS_08_CM_3M", "CMTS_09_CM_3M", "CMTS_10_CM_3M", "CMTS_11_CM_3M", "CMTS_12_CM_3M", "CMTS_13_CM_3M", "CMTS_14_CM_3M", "CMTS_15_CM_3M", "CMTS_16_CM_3M", "CMTS_17_CM_3M", "CMTS_18_CM_3M", "CMTS_19_CM_3M", "CMTS_20_CM_3M", "CMTS_21_CM_3M", "CMTS_22_CM_3M", "CMTS_23_CM_3M", "CMTS_25_CM_3M", "CMTS_26_CM_3M", "CMTS_27_CM_3M", "CMTS_28_CM_3M", "CMTS_29_CM_3M", "CMTS_34_CM_3M", "CMTS_36_CM_3M", "CMTS_37_CM_3M", "CMTS_38_CM_3M", "CMTS_39_CM_3M", "CMTS_40_CM_3M", "CMTS_41_CM_3M", "CMTS_42_CM_swkpbz", "CMTS_33_CM_3M", "CMTS_32_CM_3M", "CMTS_73_CM_swkpbz", "CMTS_74_CM_swkpbz", "CMTS_75_CM_swkpbz", "CMTS_77_CM_swkpbz", "CMTS_79_CM_swkpbz", "CMTS_81_CM_swkpbz", "CMTS_82_CM_swkpbz", "CMTS_84_CM_swkpbz", "CMTS_88_CM_swkpbz", "CMTS_89_CM_swkpbz", "CMTS_91_CM_swkpbz", "CMTS_92_CM_swkpbz", "CMTS_100_CM_swkpbz", "CMTS_104_CM_swkpbz", "CMTS_105_CM_swkpbz", "CMTS_111_CM_swkpbz", "CMTS_103_CM_swkpbz", "CMTS_106_CM_swkpbz", "CMTS_107_CM_swkpbz", "CMTS_108_CM_swkpbz" };
        /*string[] jf3Mjingtai = new string[] { "CMTS_01_CM_3M_static", "CMTS_02_CM_3M_static", "CMTS_03_CM_3M_static", "CMTS_04_CM_3M_static", "CMTS_05_CM_3M_static", "CMTS_06_CM_3M_static", "CMTS_07_CM_3M_static", "CMTS_08_CM_3M_static", "CMTS_09_CM_3M_static", "CMTS_10_CM_3M_static", "CMTS_11_CM_3M_static", "CMTS_12_CM_3M_static", "CMTS_13_CM_3M_static", "CMTS_14_CM_3M_static", "CMTS_15_CM_3M_static", "CMTS_16_CM_3M_static", "CMTS_17_CM_3M_static", "CMTS_18_CM_3M_static", "CMTS_19_CM_3M_static", "CMTS_20_CM_3M_static", "CMTS_21_CM_3M_static", "CMTS_22_CM_3M_static", "CMTS_23_CM_3M_static", "CMTS_25_CM_3M_static", "CMTS_26_CM_3M_static", "CMTS_27_CM_3M_static", "CMTS_28_CM_3M_static", "CMTS_29_CM_3M_static", "CMTS_34_CM_3M_static", "CMTS_36_CM_3M_static", "CMTS_37_CM_3M_static", "CMTS_38_CM_3M_static", "CMTS_39_CM_3M_static", "CMTS_40_CM_3M_static", "CMTS_41_CM_3M_static", "CMTS_42_CM_swkpbz_static", "CMTS_33_CM_3M_static", "CMTS_32_CM_3M_static", "CMTS_73_CM_swkpbz_static", "CMTS_74_CM_swkpbz_static", "CMTS_75_CM_swkpbz_static", "CMTS_77_CM_swkpbz_static", "CMTS_79_CM_swkpbz_static", "CMTS_81_CM_swkpbz_static", "CMTS_82_CM_swkpbz_static", "CMTS_84_CM_swkpbz_static", "CMTS_88_CM_swkpbz_static", "CMTS_89_CM_swkpbz_static", "CMTS_91_CM_swkpbz_static", "CMTS_92_CM_swkpbz_static", "CMTS_100_CM_swkpbz_static", "CMTS_104_CM_swkpbz_static", "CMTS_105_CM_swkpbz_static", "CMTS_111_CM_swkpbz_static", "CMTS_103_CM_swkpbz_static", "CMTS_106_CM_swkpbz_static", "CMTS_107_CM_swkpbz_static", "CMTS_108_CM_swkpbz_static" };
        string[] jf6M = new string[] { "CMTS_01_CM_6M", "CMTS_02_CM_6M", "CMTS_03_CM_6M", "CMTS_04_CM_6M", "CMTS_05_CM_6M", "CMTS_06_CM_6M", "CMTS_07_CM_6M", "CMTS_08_CM_6M", "CMTS_09_CM_6M", "CMTS_10_CM_6M", "CMTS_11_CM_6M", "CMTS_12_CM_6M", "CMTS_13_CM_6M", "CMTS_14_CM_6M", "CMTS_15_CM_6M", "CMTS_16_CM_6M", "CMTS_17_CM_6M", "CMTS_18_CM_6M", "CMTS_19_CM_6M", "CMTS_20_CM_6M", "CMTS_21_CM_6M", "CMTS_22_CM_6M", "CMTS_23_CM_6M", "CMTS_25_CM_6M", "CMTS_26_CM_6M", "CMTS_27_CM_6M", "CMTS_28_CM_6M", "CMTS_29_CM_6M", "CMTS_34_CM_6M", "CMTS_36_CM_6M", "CMTS_37_CM_6M", "CMTS_38_CM_6M", "CMTS_39_CM_6M", "CMTS_40_CM_6M", "CMTS_41_CM_6M", "CMTS_42_CM_swkpzz", "CMTS_33_CM_6M", "CMTS_32_CM_6M", "CMTS_73_CM_swkpzz", "CMTS_74_CM_swkpzz", "CMTS_75_CM_swkpzz", "CMTS_77_CM_swkpzz", "CMTS_79_CM_swkpzz", "CMTS_81_CM_swkpzz", "CMTS_82_CM_swkpzz", "CMTS_84_CM_swkpzz", "CMTS_88_CM_swkpzz", "CMTS_89_CM_swkpzz", "CMTS_91_CM_swkpzz", "CMTS_92_CM_swkpzz", "CMTS_100_CM_swkpzz", "CMTS_104_CM_swkpzz", "CMTS_105_CM_swkpzz", "CMTS_111_CM_swkpzz", "CMTS_103_CM_swkpzz", "CMTS_106_CM_swkpzz", "CMTS_107_CM_swkpzz", "CMTS_108_CM_swkpzz" };
        string[] jf6Mjingtai = new string[] { "CMTS_01_CM_6M_static", "CMTS_02_CM_6M_static", "CMTS_03_CM_6M_static", "CMTS_04_CM_6M_static", "CMTS_05_CM_6M_static", "CMTS_06_CM_6M_static", "CMTS_07_CM_6M_static", "CMTS_08_CM_6M_static", "CMTS_09_CM_6M_static", "CMTS_10_CM_6M_static", "CMTS_11_CM_6M_static", "CMTS_12_CM_6M_static", "CMTS_13_CM_6M_static", "CMTS_14_CM_6M_static", "CMTS_15_CM_6M_static", "CMTS_16_CM_6M_static", "CMTS_17_CM_6M_static", "CMTS_18_CM_6M_static", "CMTS_19_CM_6M_static", "CMTS_20_CM_6M_static", "CMTS_21_CM_6M_static", "CMTS_22_CM_6M_static", "CMTS_23_CM_6M_static", "CMTS_25_CM_6M_static", "CMTS_26_CM_6M_static", "CMTS_27_CM_6M_static", "CMTS_28_CM_6M_static", "CMTS_29_CM_6M_static", "CMTS_34_CM_6M_static", "CMTS_36_CM_6M_static", "CMTS_37_CM_6M_static", "CMTS_38_CM_6M_static", "CMTS_39_CM_6M_static", "CMTS_40_CM_6M_static", "CMTS_41_CM_6M_static", "CMTS_42_CM_swkpzz_static", "CMTS_33_CM_6M_static","CMTS_32_CM_6M_static", "CMTS_73_CM_swkpzz_static", "CMTS_74_CM_swkpzz_static", "CMTS_75_CM_swkpzz_static", "CMTS_77_CM_swkpzz_static", "CMTS_79_CM_swkpzz_static", "CMTS_81_CM_swkpzz_static", "CMTS_82_CM_swkpzz_static", "CMTS_84_CM_swkpzz_static", "CMTS_88_CM_swkpzz_static", "CMTS_89_CM_swkpzz_static", "CMTS_91_CM_swkpzz_static", "CMTS_92_CM_swkpzz_static", "CMTS_100_CM_swkpzz_static", "CMTS_104_CM_swkpzz_static", "CMTS_105_CM_swkpzz_static", "CMTS_111_CM_swkpzz_static", "CMTS_103_CM_swkpzz_static", "CMTS_106_CM_swkpzz_static", "CMTS_107_CM_swkpzz_static", "CMTS_108_CM_swkpzz_static" };
        string[] jf8M = new string[] { "CMTS_01_CM_8M", "CMTS_02_CM_8M", "CMTS_03_CM_8M", "CMTS_04_CM_8M", "CMTS_05_CM_8M", "CMTS_06_CM_8M", "CMTS_07_CM_8M", "CMTS_08_CM_8M", "CMTS_09_CM_8M", "CMTS_10_CM_8M", "CMTS_11_CM_8M", "CMTS_12_CM_8M", "CMTS_13_CM_8M", "CMTS_14_CM_8M", "CMTS_15_CM_8M", "CMTS_16_CM_8M", "CMTS_17_CM_8M", "CMTS_18_CM_8M", "CMTS_19_CM_8M", "CMTS_20_CM_8M", "CMTS_21_CM_8M", "CMTS_22_CM_8M", "CMTS_23_CM_8M", "CMTS_25_CM_8M", "CMTS_26_CM_8M", "CMTS_27_CM_8M", "CMTS_28_CM_8M", "CMTS_29_CM_8M", "CMTS_34_CM_8M", "CMTS_36_CM_8M", "CMTS_37_CM_8M", "CMTS_38_CM_8M", "CMTS_39_CM_8M", "CMTS_40_CM_8M", "CMTS_41_CM_8M", "CMTS_42_CM_changyou", "CMTS_33_CM_8M","CMTS_32_CM_8M", "CMTS_73_CM_changyou", "CMTS_74_CM_changyou", "CMTS_75_CM_changyou", "CMTS_77_CM_changyou", "CMTS_79_CM_changyou", "CMTS_81_CM_changyou", "CMTS_82_CM_changyou", "CMTS_84_CM_changyou", "CMTS_88_CM_changyou", "CMTS_89_CM_changyou", "CMTS_91_CM_changyou", "CMTS_92_CM_changyou", "CMTS_100_CM_changyou", "CMTS_104_CM_changyou", "CMTS_105_CM_changyou", "CMTS_111_CM_changyou", "CMTS_103_CM_changyou", "CMTS_106_CM_changyou", "CMTS_107_CM_changyou", "CMTS_108_CM_changyou" };
        string[] jf8Mjingtai = new string[] { "CMTS_01_CM_8M_static", "CMTS_02_CM_8M_static", "CMTS_03_CM_8M_static", "CMTS_04_CM_8M_static", "CMTS_05_CM_8M_static", "CMTS_06_CM_8M_static", "CMTS_07_CM_8M_static", "CMTS_08_CM_8M_static", "CMTS_09_CM_8M_static", "CMTS_10_CM_8M_static", "CMTS_11_CM_8M_static", "CMTS_12_CM_8M_static", "CMTS_13_CM_8M_static", "CMTS_14_CM_8M_static", "CMTS_15_CM_8M_static", "CMTS_16_CM_8M_static", "CMTS_17_CM_8M_static", "CMTS_18_CM_8M_static", "CMTS_19_CM_8M_static", "CMTS_20_CM_8M_static", "CMTS_21_CM_8M_static", "CMTS_22_CM_8M_static", "CMTS_23_CM_8M_static", "CMTS_25_CM_8M_static", "CMTS_26_CM_8M_static", "CMTS_27_CM_8M_static", "CMTS_28_CM_8M_static", "CMTS_29_CM_8M_static", "CMTS_34_CM_8M_static", "CMTS_36_CM_8M_static", "CMTS_37_CM_8M_static", "CMTS_38_CM_8M_static", "CMTS_39_CM_8M_static", "CMTS_40_CM_8M_static", "CMTS_41_CM_8M_static", "CMTS_42_CM_changyou_static", "CMTS_33_CM_8M_static","CMTS_32_CM_8M_static", "CMTS_73_CM_8M_static", "CMTS_74_CM_8M_static", "CMTS_75_CM_8M_static", "CMTS_77_CM_8M_static", "CMTS_79_CM_8M_static", "CMTS_81_CM_8M_static", "CMTS_82_CM_8M_static", "CMTS_84_CM_8M_static", "CMTS_88_CM_8M_static", "CMTS_89_CM_8M_static", "CMTS_91_CM_8M_static", "CMTS_92_CM_8M_static", "CMTS_100_CM_8M_static", "CMTS_104_CM_8M_static", "CMTS_105_CM_8M_static", "CMTS_111_CM_8M_static", "CMTS_103_CM_changyou_static", "CMTS_106_CM_changyou_static", "CMTS_107_CM_changyou_static", "CMTS_108_CM_changyou_static" };*/
        string[] jfjiaqi = new string[] { "CMTS_01_CM_jiaqi", "CMTS_02_CM_jiaqi", "CMTS_03_CM_jiaqi", "CMTS_04_CM_jiaqi", "CMTS_05_CM_jiaqi", "CMTS_06_CM_jiaqi", "CMTS_07_CM_jiaqi", "CMTS_08_CM_jiaqi", "CMTS_09_CM_jiaqi", "CMTS_10_CM_jiaqi", "CMTS_12_CM_jiaqi", "CMTS_13_CM_jiaqi", "CMTS_14_CM_jiaqi", "CMTS_15_CM_jiaqi", "CMTS_16_CM_jiaqi", "CMTS_17_CM_jiaqi", "CMTS_18_CM_jiaqi", "CMTS_19_CM_jiaqi", "CMTS_20_CM_jiaqi", "CMTS_21_CM_jiaqi", "CMTS_22_CM_jiaqi", "CMTS_23_CM_jiaqi", "CMTS_25_CM_jiaqi", "CMTS_26_CM_jiaqi", "CMTS_27_CM_jiaqi", "CMTS_28_CM_jiaqi", "CMTS_29_CM_jiaqi", "CMTS_34_CM_jiaqi", "CMTS_36_CM_jiaqi", "CMTS_37_CM_jiaqi", "CMTS_38_CM_jiaqi", "CMTS_39_CM_jiaqi", "CMTS_40_CM_jiaqi", "CMTS_41_CM_jiaqi", "CMTS_42_CM_jiaqi", "CMTS_33_CM_jiaqi", "CMTS_32_CM_jiaqi", "CMTS_73_CM_jiaqi", "CMTS_74_CM_jiaqi", "CMTS_75_CM_jiaqi", "CMTS_77_CM_jiaqi", "CMTS_79_CM_jiaqi", "CMTS_81_CM_jiaqi", "CMTS_82_CM_jiaqi", "CMTS_84_CM_jiaqi", "CMTS_88_CM_jiaqi", "CMTS_89_CM_jiaqi", "CMTS_91_CM_jiaqi", "CMTS_92_CM_jiaqi", "CMTS_100_CM_jiaqi", "CMTS_104_CM_jiaqi", "CMTS_105_CM_jiaqi", "CMTS_111_CM_jiaqi", "CMTS_103_CM_jiaqi", "CMTS_106_CM_jiaqi", "CMTS_107_CM_jiaqi", "CMTS_108_CM_jiaqi" };
        string[] jfCMeconomy = new string[] { "CMTS_01_CM_economy", "CMTS_02_CM_economy", "CMTS_03_CM_economy", "CMTS_04_CM_economy", "CMTS_05_CM_economy", "CMTS_06_CM_economy", "CMTS_07_CM_economy", "CMTS_08_CM_economy", "CMTS_09_CM_economy", "CMTS_10_CM_economy", "CMTS_12_CM_economy", "CMTS_13_CM_economy", "CMTS_14_CM_economy", "CMTS_15_CM_economy", "CMTS_16_CM_economy", "CMTS_17_CM_economy", "CMTS_18_CM_economy", "CMTS_19_CM_economy", "CMTS_20_CM_economy", "CMTS_21_CM_economy", "CMTS_22_CM_economy", "CMTS_23_CM_economy", "CMTS_25_CM_economy", "CMTS_26_CM_economy", "CMTS_27_CM_economy", "CMTS_28_CM_economy", "CMTS_29_CM_economy", "CMTS_34_CM_economy", "CMTS_36_CM_economy", "CMTS_37_CM_economy", "CMTS_38_CM_economy", "CMTS_39_CM_economy", "CMTS_40_CM_economy", "CMTS_41_CM_economy", "CMTS_42_CM_economy", "CMTS_33_CM_economy", "CMTS_32_CM_economy", "CMTS_73_CM_economy", "CMTS_74_CM_economy", "CMTS_75_CM_economy", "CMTS_77_CM_economy", "CMTS_79_CM_economy", "CMTS_81_CM_economy", "CMTS_82_CM_economy", "CMTS_84_CM_economy", "CMTS_88_CM_economy", "CMTS_89_CM_economy", "CMTS_91_CM_economy", "CMTS_92_CM_economy", "CMTS_100_CM_economy", "CMTS_104_CM_economy", "CMTS_105_CM_economy", "CMTS_111_CM_economy", "CMTS_103_CM_economy", "CMTS_106_CM_economy", "CMTS_107_CM_economy", "CMTS_108_CM_economy" };
        string[] jfCPEeconomy = new string[] { "CMTS_01_CPE_economy", "CMTS_02_CPE_economy", "CMTS_03_CPE_economy", "CMTS_04_CPE_economy", "CMTS_05_CPE_economy", "CMTS_06_CPE_economy", "CMTS_07_CPE_economy", "CMTS_08_CPE_economy", "CMTS_09_CPE_economy", "CMTS_10_CPE_economy", "CMTS_12_CPE_economy", "CMTS_13_CPE_economy", "CMTS_14_CPE_economy", "CMTS_15_CPE_economy", "CMTS_16_CPE_economy", "CMTS_17_CPE_economy", "CMTS_18_CPE_economy", "CMTS_19_CPE_economy", "CMTS_20_CPE_economy", "CMTS_21_CPE_economy", "CMTS_22_CPE_economy", "CMTS_23_CPE_economy", "CMTS_25_CPE_economy", "CMTS_26_CPE_economy", "CMTS_27_CPE_economy", "CMTS_28_CPE_economy", "CMTS_29_CPE_economy", "CMTS_34_CPE_economy", "CMTS_36_CPE_economy", "CMTS_37_CPE_economy", "CMTS_38_CPE_economy", "CMTS_39_CPE_economy", "CMTS_40_CPE_economy", "CMTS_41_CPE_economy", "CMTS_42_CPE_economy",  "CMTS_33_CPE_economy", "CMTS_32_CPE_economy", "CMTS_73_CPE_economy", "CMTS_74_CPE_economy", "CMTS_75_CPE_economy", "CMTS_77_CPE_economy", "CMTS_79_CPE_economy", "CMTS_81_CPE_economy", "CMTS_82_CPE_economy", "CMTS_84_CPE_economy", "CMTS_88_CPE_economy", "CMTS_89_CPE_economy", "CMTS_91_CPE_economy", "CMTS_92_CPE_economy", "CMTS_100_CPE_economy", "CMTS_104_CPE_economy", "CMTS_105_CPE_economy", "CMTS_111_CPE_economy", "CMTS_103_CPE_economy", "CMTS_106_CPE_economy", "CMTS_107_CPE_economy", "CMTS_108_CPE_economy" };
        string[] jf1IP = new string[] { "CMTS_01_CM_1IP", "CMTS_02_CM_1IP", "CMTS_03_CM_1IP", "CMTS_04_CM_1IP", "CMTS_05_CM_1IP", "CMTS_06_CM_1IP", "CMTS_07_CM_1IP", "CMTS_08_CM_1IP", "CMTS_09_CM_1IP", "CMTS_10_CM_1IP",  "CMTS_12_CM_1IP", "CMTS_13_CM_1IP", "CMTS_14_CM_1IP", "CMTS_15_CM_1IP", "CMTS_16_CM_1IP", "CMTS_17_CM_1IP", "CMTS_18_CM_1IP", "CMTS_19_CM_1IP", "CMTS_20_CM_1IP", "CMTS_21_CM_1IP", "CMTS_22_CM_1IP", "CMTS_23_CM_1IP", "CMTS_25_CM_1IP", "CMTS_26_CM_1IP", "CMTS_27_CM_1IP", "CMTS_28_CM_1IP", "CMTS_29_CM_1IP", "CMTS_34_CM_1IP", "CMTS_36_CM_1IP", "CMTS_37_CM_1IP", "CMTS_38_CM_1IP", "CMTS_39_CM_1IP", "CMTS_40_CM_1IP", "CMTS_41_CM_1IP", "CMTS_42_ehome_jingdian_CM_1IP", "CMTS_33_CM_1IP", "CMTS_32_CM_1IP", "CMTS_73_ehome_jingdian_CM_1IP", "CMTS_74_ehome_jingdian_CM_1IP", "CMTS_75_ehome_jingdian_CM_1IP", "CMTS_77_ehome_jingdian_1IP", "CMTS_79_ehome_jingdian_CM_1IP", "CMTS_81_ehome_jingdian_CM_1IP", "CMTS_82_ehome_jingdian_CM_1IP", "CMTS_84_ehome_jingdian_CM_1IP", "CMTS_88_ehome_jingdian_CM_1IP", "CMTS_89_ehome_jingdian_CM_1IP", "CMTS_91_ehome_jingdian_CM_1IP", "CMTS_92_ehome_jingdian_CM_1IP", "CMTS_100_ehome_jingdian_1IP", "CMTS_104_ehome_jingdian_1IP", "CMTS_105_ehome_jingdian_1IP", "CMTS_111_ehome_jingdian_1IP", "CMTS_103_ehome_jingdian_1IP", "CMTS_106_ehome_jingdian_1IP", "CMTS_107_ehome_jingdian_1IP", "CMTS_108_ehome_jingdian_1IP" };
        string[] jf2IP = new string[] { "CMTS_01_CM_2IP", "CMTS_02_CM_2IP", "CMTS_03_CM_2IP", "CMTS_04_CM_2IP", "CMTS_05_CM_2IP", "CMTS_06_CM_2IP", "CMTS_07_CM_2IP", "CMTS_08_CM_2IP", "CMTS_09_CM_2IP", "CMTS_10_CM_2IP", "CMTS_12_CM_2IP", "CMTS_13_CM_2IP", "CMTS_14_CM_2IP", "CMTS_15_CM_2IP", "CMTS_16_CM_2IP", "CMTS_17_CM_2IP", "CMTS_18_CM_2IP", "CMTS_19_CM_2IP", "CMTS_20_CM_2IP", "CMTS_21_CM_2IP", "CMTS_22_CM_2IP", "CMTS_23_CM_2IP", "CMTS_25_CM_2IP", "CMTS_26_CM_2IP", "CMTS_27_CM_2IP", "CMTS_28_CM_2IP", "CMTS_29_CM_2IP", "CMTS_34_CM_2IP", "CMTS_36_CM_2IP", "CMTS_37_CM_2IP", "CMTS_38_CM_2IP", "CMTS_39_CM_2IP", "CMTS_40_CM_2IP", "CMTS_41_CM_2IP", "CMTS_42_ehome_jingdian_CM_2IP",  "CMTS_33_CM_2IP", "CMTS_32_CM_2IP", "CMTS_73_ehome_jingdian_CM_2IP", "CMTS_74_ehome_jingdian_CM_2IP", "CMTS_75_ehome_jingdian_CM_2IP", "CMTS_77_ehome_jingdian_2IP", "CMTS_79_ehome_jingdian_CM_2IP", "CMTS_81_ehome_jingdian_CM_2IP", "CMTS_82_ehome_jingdian_CM_2IP", "CMTS_84_ehome_jingdian_CM_2IP", "CMTS_88_ehome_jingdian_CM_2IP", "CMTS_89_ehome_jingdian_CM_2IP", "CMTS_91_ehome_jingdian_CM_2IP", "CMTS_92_ehome_jingdian_CM_2IP", "CMTS_100_ehome_jingdian_2IP", "CMTS_104_ehome_jingdian_2IP", "CMTS_105_ehome_jingdian_2IP", "CMTS_111_ehome_jingdian_2IP", "CMTS_103_ehome_jingdian_2IP", "CMTS_106_ehome_jingdian_2IP", "CMTS_107_ehome_jingdian_2IP", "CMTS_108_ehome_jingdian_2IP" };
        //string[] jfzhuanye = new string[] { "CMTS_01_CM_zhuanye", "CMTS_02_CM_zhuanye", "CMTS_03_CM_zhuanye", "CMTS_04_CM_zhuanye", "CMTS_05_CM_zhuanye", "CMTS_06_CM_zhuanye", "CMTS_07_CM_zhuanye", "CMTS_08_CM_zhuanye", "CMTS_09_CM_zhuanye", "CMTS_10_CM_zhuanye", "CMTS_11_CM_zhuanye", "CMTS_12_CM_zhuanye", "CMTS_13_CM_zhuanye", "CMTS_14_CM_zhuanye", "CMTS_15_CM_zhuanye", "CMTS_16_CM_zhuanye", "CMTS_17_CM_zhuanye", "CMTS_18_CM_zhuanye", "CMTS_19_CM_zhuanye", "CMTS_20_CM_zhuanye", "CMTS_21_CM_zhuanye", "CMTS_22_CM_zhuanye", "CMTS_23_CM_zhuanye", "CMTS_25_CM_zhuanye", "CMTS_26_CM_zhuanye", "CMTS_27_CM_zhuanye", "CMTS_28_CM_zhuanye", "CMTS_29_CM_zhuanye", "CMTS_34_CM_zhuanye", "CMTS_36_CM_zhuanye", "CMTS_37_CM_zhuanye", "CMTS_38_CM_zhuanye", "CMTS_39_CM_zhuanye", "CMTS_40_CM_zhuanye", "CMTS_41_CM_zhuanye", "CMTS_42_CM_zhuanye", "CMTS_33_CM_zhuanye","CMTS_32_CM_zhuanye", "CMTS_73_CM_zhuanye", "CMTS_74_CM_zhuanye", "CMTS_75_CM_zhuanye", "CMTS_77_CM_zhuanye", "CMTS_79_CM_zhuanye", "CMTS_81_CM_zhuanye", "CMTS_82_CM_zhuanye", "CMTS_84_CM_zhuanye", "CMTS_88_CM_zhuanye", "CMTS_89_CM_zhuanye", "CMTS_91_CM_zhuanye", "CMTS_92_CM_zhuanye", "CMTS_100_CM_zhuanye", "CMTS_104_CM_zhuanye", "CMTS_105_CM_zhuanye", "CMTS_111_CM_zhuanye", "CMTS_103_CM_zhuanye", "CMTS_106_CM_zhuanye", "CMTS_107_CM_zhuanye", "CMTS_108_CM_zhuanye" };
        string[] jf4M1IP = new string[] { "CMTS_01_ehome_4M_1IP", "CMTS_02_ehome_4M_1IP", "CMTS_03_ehome_4M_1IP", "CMTS_04_ehome_4M_1IP", "CMTS_05_ehome_4M_1IP", "CMTS_06_ehome_4M_1IP", "CMTS_07_ehome_4M_1IP", "CMTS_08_ehome_4M_1IP", "CMTS_09_ehome_4M_1IP", "CMTS_10_ehome_4M_1IP", "CMTS_12_ehome_4M_1IP", "CMTS_13_ehome_4M_1IP", "CMTS_14_ehome_4M_1IP", "CMTS_15_ehome_4M_1IP", "CMTS_16_ehome_4M_1IP", "CMTS_17_ehome_4M_1IP", "CMTS_18_ehome_4M_1IP", "CMTS_19_ehome_4M_1IP", "CMTS_20_ehome_4M_1IP", "CMTS_21_ehome_4M_1IP", "CMTS_22_ehome_4M_1IP", "CMTS_23_ehome_4M_1IP", "CMTS_25_ehome_4M_1IP", "CMTS_26_ehome_4M_1IP", "CMTS_27_ehome_4M_1IP", "CMTS_28_ehome_4M_1IP", "CMTS_29_ehome_4M_1IP", "CMTS_34_ehome_4M_1IP", "CMTS_36_ehome_4M_1IP", "CMTS_37_ehome_4M_1IP", "CMTS_38_ehome_4M_1IP", "CMTS_39_ehome_4M_1IP", "CMTS_40_ehome_4M_1IP", "CMTS_41_ehome_4M_1IP", "CMTS_42_ehome_jingying_1IP", "CMTS_33_ehome_4M_1IP", "CMTS_32_ehome_4M_1IP", "CMTS_73_ehome_jingying_1IP", "CMTS_74_ehome_jingying_1IP", "CMTS_75_ehome_jingying_1IP", "CMTS_77_ehome_jingying_1IP", "CMTS_79_ehome_jingying_1IP", "CMTS_81_ehome_jingying_1IP", "CMTS_82_ehome_jingying_1IP", "CMTS_84_ehome_jingying_1IP", "CMTS_88_ehome_jingying_1IP", "CMTS_89_ehome_jingying_1IP", "CMTS_91_ehome_jingying_1IP", "CMTS_92_ehome_jingying_1IP", "CMTS_100_ehome_jingying_1IP", "CMTS_104_ehome_jingying_1IP", "CMTS_105_ehome_jingying_1IP", "CMTS_111_ehome_jingying_1IP", "CMTS_103_ehome_jingying_1IP", "CMTS_106_ehome_jingying_1IP", "CMTS_107_ehome_jingying_1IP", "CMTS_108_ehome_jingying_1IP" };
        string[] jf4M2IP = new string[] { "CMTS_01_ehome_4M_2IP", "CMTS_02_ehome_4M_2IP", "CMTS_03_ehome_4M_2IP", "CMTS_04_ehome_4M_2IP", "CMTS_05_ehome_4M_2IP", "CMTS_06_ehome_4M_2IP", "CMTS_07_ehome_4M_2IP", "CMTS_08_ehome_4M_2IP", "CMTS_09_ehome_4M_2IP", "CMTS_10_ehome_4M_2IP",  "CMTS_12_ehome_4M_2IP", "CMTS_13_ehome_4M_2IP", "CMTS_14_ehome_4M_2IP", "CMTS_15_ehome_4M_2IP", "CMTS_16_ehome_4M_2IP", "CMTS_17_ehome_4M_2IP", "CMTS_18_ehome_4M_2IP", "CMTS_19_ehome_4M_2IP", "CMTS_20_ehome_4M_2IP", "CMTS_21_ehome_4M_2IP", "CMTS_22_ehome_4M_2IP", "CMTS_23_ehome_4M_2IP", "CMTS_25_ehome_4M_2IP", "CMTS_26_ehome_4M_2IP", "CMTS_27_ehome_4M_2IP", "CMTS_28_ehome_4M_2IP", "CMTS_29_ehome_4M_2IP", "CMTS_34_ehome_4M_2IP", "CMTS_36_ehome_4M_2IP", "CMTS_37_ehome_4M_2IP", "CMTS_38_ehome_4M_2IP", "CMTS_39_ehome_4M_2IP", "CMTS_40_ehome_4M_2IP", "CMTS_41_ehome_4M_2IP", "CMTS_42_ehome_jingying_2IP", "CMTS_33_ehome_4M_2IP", "CMTS_32_ehome_4M_2IP", "CMTS_73_ehome_jingying_2IP", "CMTS_74_ehome_jingying_2IP", "CMTS_75_ehome_jingying_2IP", "CMTS_77_ehome_jingying_2IP", "CMTS_79_ehome_jingying_2IP", "CMTS_81_ehome_jingying_2IP", "CMTS_82_ehome_jingying_2IP", "CMTS_84_ehome_jingying_2IP", "CMTS_88_ehome_jingying_2IP", "CMTS_89_ehome_jingying_2IP", "CMTS_91_ehome_jingying_2IP", "CMTS_92_ehome_jingying_2IP", "CMTS_100_ehome_jingying_2IP", "CMTS_104_ehome_jingying_2IP", "CMTS_105_ehome_jingying_2IP", "CMTS_111_ehome_jingying_2IP", "CMTS_103_ehome_jingying_2IP", "CMTS_106_ehome_jingying_2IP", "CMTS_107_ehome_jingying_2IP", "CMTS_108_ehome_jingying_2IP" };
        string[] jf8M1IP = new string[] { "CMTS_01_ehome_8M_1IP", "CMTS_02_ehome_8M_1IP", "CMTS_03_ehome_8M_1IP", "CMTS_04_ehome_8M_1IP", "CMTS_05_ehome_8M_1IP", "CMTS_06_ehome_8M_1IP", "CMTS_07_ehome_8M_1IP", "CMTS_08_ehome_8M_1IP", "CMTS_09_ehome_8M_1IP", "CMTS_10_ehome_8M_1IP",  "CMTS_12_ehome_8M_1IP", "CMTS_13_ehome_8M_1IP", "CMTS_14_ehome_8M_1IP", "CMTS_15_ehome_8M_1IP", "CMTS_16_ehome_8M_1IP", "CMTS_17_ehome_8M_1IP", "CMTS_18_ehome_8M_1IP", "CMTS_19_ehome_8M_1IP", "CMTS_20_ehome_8M_1IP", "CMTS_21_ehome_8M_1IP", "CMTS_22_ehome_8M_1IP", "CMTS_23_ehome_8M_1IP", "CMTS_25_ehome_8M_1IP", "CMTS_26_ehome_8M_1IP", "CMTS_27_ehome_8M_1IP", "CMTS_28_ehome_8M_1IP", "CMTS_29_ehome_8M_1IP", "CMTS_34_ehome_8M_1IP", "CMTS_36_ehome_8M_1IP", "CMTS_37_ehome_8M_1IP", "CMTS_38_ehome_8M_1IP", "CMTS_39_ehome_8M_1IP", "CMTS_40_ehome_8M_1IP", "CMTS_41_ehome_8M_1IP", "CMTS_42_ehome_changyou_1IP", "CMTS_33_ehome_8M_1IP", "CMTS_32_ehome_8M_1IP", "CMTS_73_ehome_changyou_1IP", "CMTS_74_ehome_changyou_1IP", "CMTS_75_ehome_changyou_1IP", "CMTS_77_ehome_changyou_1IP", "CMTS_79_ehome_changyou_1IP", "CMTS_81_ehome_changyou_1IP", "CMTS_82_ehome_changyou_1IP", "CMTS_84_ehome_changyou_1IP", "CMTS_88_ehome_changyou_1IP", "CMTS_89_ehome_changyou_1IP", "CMTS_91_ehome_changyou_1IP", "CMTS_92_ehome_changyou_1IP", "CMTS_100_ehome_changyou_1IP", "CMTS_104_ehome_changyou_1IP", "CMTS_105_ehome_changyou_1IP", "CMTS_111_ehome_changyou_1IP", "CMTS_103_ehome_changyou_1IP", "CMTS_106_ehome_changyou_1IP", "CMTS_107_ehome_changyou_1IP", "CMTS_108_ehome_changyou_1IP" };
        string[] jf8M2IP = new string[] { "CMTS_01_ehome_8M_2IP", "CMTS_02_ehome_8M_2IP", "CMTS_03_ehome_8M_2IP", "CMTS_04_ehome_8M_2IP", "CMTS_05_ehome_8M_2IP", "CMTS_06_ehome_8M_2IP", "CMTS_07_ehome_8M_2IP", "CMTS_08_ehome_8M_2IP", "CMTS_09_ehome_8M_2IP", "CMTS_10_ehome_8M_2IP", "CMTS_12_ehome_8M_2IP", "CMTS_13_ehome_8M_2IP", "CMTS_14_ehome_8M_2IP", "CMTS_15_ehome_8M_2IP", "CMTS_16_ehome_8M_2IP", "CMTS_17_ehome_8M_2IP", "CMTS_18_ehome_8M_2IP", "CMTS_19_ehome_8M_2IP", "CMTS_20_ehome_8M_2IP", "CMTS_21_ehome_8M_2IP", "CMTS_22_ehome_8M_2IP", "CMTS_23_ehome_8M_2IP", "CMTS_25_ehome_8M_2IP", "CMTS_26_ehome_8M_2IP", "CMTS_27_ehome_8M_2IP", "CMTS_28_ehome_8M_2IP", "CMTS_29_ehome_8M_2IP", "CMTS_34_ehome_8M_2IP", "CMTS_36_ehome_8M_2IP", "CMTS_37_ehome_8M_2IP", "CMTS_38_ehome_8M_2IP", "CMTS_39_ehome_8M_2IP", "CMTS_40_ehome_8M_2IP", "CMTS_41_ehome_8M_2IP", "CMTS_42_ehome_changyou_2IP", "CMTS_33_ehome_8M_2IP", "CMTS_32_ehome_8M_2IP", "CMTS_73_ehome_changyou_2IP", "CMTS_74_ehome_changyou_2IP", "CMTS_75_ehome_changyou_2IP", "CMTS_77_ehome_changyou_2IP", "CMTS_79_ehome_changyou_2IP", "CMTS_81_ehome_changyou_2IP", "CMTS_82_ehome_changyou_2IP", "CMTS_84_ehome_changyou_2IP", "CMTS_88_ehome_changyou_2IP", "CMTS_89_ehome_changyou_2IP", "CMTS_91_ehome_changyou_2IP", "CMTS_92_ehome_changyou_2IP", "CMTS_100_ehome_changyou_2IP", "CMTS_104_ehome_changyou_2IP", "CMTS_105_ehome_changyou_2IP", "CMTS_111_ehome_changyou_2IP", "CMTS_103_ehome_changyou_2IP", "CMTS_106_ehome_changyou_2IP", "CMTS_107_ehome_changyou_2IP", "CMTS_108_ehome_changyou_2IP" };
        //string[] jf30M = new string[] { "CMTS_01_CM_30M", "CMTS_02_CM_30M", "CMTS_03_CM_30M", "CMTS_04_CM_30M", "CMTS_05_CM_30M", "CMTS_06_CM_30M", "CMTS_07_CM_30M", "CMTS_08_CM_30M", "CMTS_09_CM_30M", "CMTS_10_CM_30M", "CMTS_11_CM_30M", "CMTS_12_CM_30M", "CMTS_13_CM_30M", "CMTS_14_CM_30M", "CMTS_15_CM_30M", "CMTS_16_CM_30M", "CMTS_17_CM_30M", "CMTS_18_CM_30M", "CMTS_19_CM_30M", "CMTS_20_CM_30M", "CMTS_21_CM_30M", "CMTS_22_CM_30M", "CMTS_23_CM_30M", "CMTS_25_CM_30M", "CMTS_26_CM_30M", "CMTS_27_CM_30M", "CMTS_28_CM_30M", "CMTS_29_CM_30M", "CMTS_34_CM_30M", "CMTS_36_CM_30M", "CMTS_37_CM_30M", "CMTS_38_CM_30M", "CMTS_39_CM_30M", "CMTS_40_CM_30M", "CMTS_41_CM_30M", "CMTS_42_CM_zhuoyue", "CMTS_33_CM_30M","CMTS_32_CM_30M", "CMTS_73_CM_zhuoyue", "CMTS_74_CM_zhuoyue", "CMTS_75_CM_zhuoyue", "CMTS_77_CM_zhuoyue", "CMTS_79_CM_zhuoyue", "CMTS_81_CM_zhuoyue", "CMTS_82_CM_zhuoyue", "CMTS_84_CM_zhuoyue", "CMTS_88_CM_zhuoyue", "CMTS_89_CM_zhuoyue", "CMTS_91_CM_zhuoyue", "CMTS_92_CM_zhuoyue", "CMTS_100_CM_zhuoyue", "CMTS_104_CM_zhuoyue", "CMTS_105_CM_zhuoyue", "CMTS_111_CM_zhuoyue", "CMTS_103_CM_zhuoyue", "CMTS_106_CM_zhuoyue", "CMTS_107_CM_zhuoyue", "CMTS_108_CM_zhuoyue" };
        //string[] jf100M = new string[] { "CMTS_01_CM_100M", "CMTS_02_CM_100M", "CMTS_03_CM_100M", "CMTS_04_CM_100M", "CMTS_05_CM_100M", "CMTS_06_CM_100M", "CMTS_07_CM_100M", "CMTS_08_CM_100M", "CMTS_09_CM_100M", "CMTS_10_CM_100M", "CMTS_11_CM_100M", "CMTS_12_CM_100M", "CMTS_13_CM_100M", "CMTS_14_CM_100M", "CMTS_15_CM_100M", "CMTS_16_CM_100M", "CMTS_17_CM_100M", "CMTS_18_CM_00M", "CMTS_19_CM_100M", "CMTS_20_CM_100M", "CMTS_21_CM_100M", "CMTS_22_CM_100M", "CMTS_23_CM_100M", "CMTS_25_CM_100M", "CMTS_26_CM_100M", "CMTS_27_CM_100M", "CMTS_28_CM_100M", "CMTS_29_CM_100M", "CMTS_34_CM_100M", "CMTS_36_CM_100M", "CMTS_37_CM_100M", "CMTS_38_CM_100M", "CMTS_39_CM_100M", "CMTS_40_CM_100M", "CMTS_41_CM_100M", "CMTS_42_CM_100M", "CMTS_33_CM_100M", "CMTS_32_CM_100M", "CMTS_73_CM_100M", "CMTS_74_CM_100M", "CMTS_75_CM_100M", "CMTS_77_CM_100M", "CMTS_79_CM_100M", "CMTS_81_CM_100M", "CMTS_82_CM_100M", "CMTS_84_CM_100M", "CMTS_88_CM_100M", "CMTS_89_CM_100M", "CMTS_91_CM_100M", "CMTS_92_CM_100M", "CMTS_100_CM_100M", "CMTS_104_CM_100M", "CMTS_105_CM_100M", "CMTS_111_CM_100M", "CMTS_103_CM_100M", "CMTS_106_CM_100M", "CMTS_107_CM_100M", "CMTS_108_CM_100M" };
        string[] ltlh6M = new string[] { "CMTS_01_CM_LTLH6", "CMTS_02_CM_LTLH6", "CMTS_03_CM_LTLH6", "CMTS_04_CM_LTLH6", "CMTS_05_CM_LTLH6", "CMTS_06_CM_LTLH6", "CMTS_07_CM_LTLH6", "CMTS_08_CM_LTLH6", "CMTS_09_CM_LTLH6", "CMTS_10_CM_LTLH6",  "CMTS_12_CM_LTLH6", "CMTS_13_CM_LTLH6", "CMTS_14_CM_LTLH6", "CMTS_15_CM_LTLH6", "CMTS_16_CM_LTLH6", "CMTS_17_CM_LTLH6", "CMTS_18_CM_00M", "CMTS_19_CM_LTLH6", "CMTS_20_CM_LTLH6", "CMTS_21_CM_LTLH6", "CMTS_22_CM_LTLH6", "CMTS_23_CM_LTLH6", "CMTS_25_CM_LTLH6", "CMTS_26_CM_LTLH6", "CMTS_27_CM_LTLH6", "CMTS_28_CM_LTLH6", "CMTS_29_CM_LTLH6", "CMTS_34_CM_LTLH6", "CMTS_36_CM_LTLH6", "CMTS_37_CM_LTLH6", "CMTS_38_CM_LTLH6", "CMTS_39_CM_LTLH6", "CMTS_40_CM_LTLH6", "CMTS_41_CM_LTLH6", "CMTS_42_CM_LTLH6", "CMTS_33_CM_LTLH6", "CMTS_32_CM_LTLH6", "CMTS_73_CM_LTLH6", "CMTS_74_CM_LTLH6", "CMTS_75_CM_LTLH6", "CMTS_77_CM_LTLH6", "CMTS_79_CM_LTLH6", "CMTS_81_CM_LTLH6", "CMTS_82_CM_LTLH6", "CMTS_84_CM_LTLH6", "CMTS_88_CM_LTLH6", "CMTS_89_CM_LTLH6", "CMTS_91_CM_LTLH6", "CMTS_92_CM_LTLH6", "CMTS_100_CM_LTLH6", "CMTS_104_CM_LTLH6", "CMTS_105_CM_LTLH6", "CMTS_111_CM_LTLH6", "CMTS_103_CM_LTLH6", "CMTS_106_CM_LTLH6", "CMTS_107_CM_LTLH6", "CMTS_108_CM_LTLH6" };
        string[] ltlh10M = new string[] { "CMTS_01_CM_LTLH10", "CMTS_02_CM_LTLH10", "CMTS_03_CM_LTLH10", "CMTS_04_CM_LTLH10", "CMTS_05_CM_LTLH10", "CMTS_06_CM_LTLH10", "CMTS_07_CM_LTLH10", "CMTS_08_CM_LTLH10", "CMTS_09_CM_LTLH10", "CMTS_10_CM_LTLH10",  "CMTS_12_CM_LTLH10", "CMTS_13_CM_LTLH10", "CMTS_14_CM_LTLH10", "CMTS_15_CM_LTLH10", "CMTS_16_CM_LTLH10", "CMTS_17_CM_LTLH10", "CMTS_18_CM_00M", "CMTS_19_CM_LTLH10", "CMTS_20_CM_LTLH10", "CMTS_21_CM_LTLH10", "CMTS_22_CM_LTLH10", "CMTS_23_CM_LTLH10", "CMTS_25_CM_LTLH10", "CMTS_26_CM_LTLH10", "CMTS_27_CM_LTLH10", "CMTS_28_CM_LTLH10", "CMTS_29_CM_LTLH10", "CMTS_34_CM_LTLH10", "CMTS_36_CM_LTLH10", "CMTS_37_CM_LTLH10", "CMTS_38_CM_LTLH10", "CMTS_39_CM_LTLH10", "CMTS_40_CM_LTLH10", "CMTS_41_CM_LTLH10", "CMTS_42_CM_LTLH10", "CMTS_33_CM_LTLH10", "CMTS_32_CM_LTLH10", "CMTS_73_CM_LTLH10", "CMTS_74_CM_LTLH10", "CMTS_75_CM_LTLH10", "CMTS_77_CM_LTLH10", "CMTS_79_CM_LTLH10", "CMTS_81_CM_LTLH10", "CMTS_82_CM_LTLH10", "CMTS_84_CM_LTLH10", "CMTS_88_CM_LTLH10", "CMTS_89_CM_LTLH10", "CMTS_91_CM_LTLH10", "CMTS_92_CM_LTLH10", "CMTS_100_CM_LTLH10", "CMTS_104_CM_LTLH10", "CMTS_105_CM_LTLH10", "CMTS_111_CM_LTLH10", "CMTS_103_CM_LTLH10", "CMTS_106_CM_LTLH10", "CMTS_107_CM_LTLH10", "CMTS_108_CM_LTLH10" };

        public Form1()
        {
            InitializeComponent();
            DateTime dt1 = DateTime.Now;
            string dt2 = dt1.GetDateTimeFormats('g')[0].ToString();
            label32.Text = "��ǰ�û���" + Form2.strName;
            label33.Text = "��¼ʱ�䣺" + dt2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            string[] str = new string[11];//20170704��Ϊ11
            //string mac = mac;
            
            //20170810
            string mac = comboBox5.Text;
            int item_count = comboBox5.Items.Count;//number of items
            if (! comboBox5.Items.Contains(mac))
            {
                comboBox5.Items.Add(comboBox5.Text);//when press "search" then add the item.

            }
            if (item_count >= 5)//set the maximum of items is 5
            {
                comboBox5.Items.RemoveAt(0);//remove the first item.(the oldest item)
            }

            string command = "list search clientclassentry hardwareaddress " + mac + "\r\n" + "list search clientclassentry remoteID " + mac + "\r\n" + "exit\r\n";
            File.Delete("D:\\CM_MAC\\sfcce.txt");
            FileStream MacFile = new FileStream("D:\\CM_MAC\\sfcce.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter MacFileWriter = new StreamWriter(MacFile);
            MacFileWriter.Flush();
            // ʹ��StreamWriter�����ļ���д������
            MacFileWriter.BaseStream.Seek(0, SeekOrigin.Begin);
            // ��richtextBox11�е�����д���ļ�
            MacFileWriter.Write(command);
            MacFileWriter.Flush();
            MacFileWriter.Close();

            //MacFileWriter.Flush();
            //MacFileWriter.Write(Comand);
            //�رմ��ļ�
            //MacFileWriter.Flush();
            //MacFileWriter.Close();

            Process l = new Process();
            l.StartInfo.FileName = "cmd.exe";
            l.StartInfo.UseShellExecute = false;
            l.StartInfo.RedirectStandardInput = true;
            l.StartInfo.RedirectStandardOutput = true;
            l.StartInfo.RedirectStandardError = true;
            l.StartInfo.CreateNoWindow = true;

            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.101 -F D:\\CM_MAC\\sfcce.txt");
            l.StandardInput.WriteLine("exit");
            str[0] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.103 -F D:\\CM_MAC\\sfcce.txt");
            l.StandardInput.WriteLine("exit");
            str[1] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.105 -F D:\\CM_MAC\\sfcce.txt");
            l.StandardInput.WriteLine("exit");
            str[2] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.107 -F D:\\CM_MAC\\sfcce.txt");
            l.StandardInput.WriteLine("exit");
            str[3] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.109 -F D:\\CM_MAC\\sfcce.txt");
            l.StandardInput.WriteLine("exit");
            str[4] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.111 -F D:\\CM_MAC\\sfcce.txt");
            l.StandardInput.WriteLine("exit");
            str[5] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.113 -F D:\\CM_MAC\\sfcce.txt");
            l.StandardInput.WriteLine("exit");
            str[6] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.87 -F D:\\CM_MAC\\sfcce.txt");
            l.StandardInput.WriteLine("exit");
            str[7] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.91 -F D:\\CM_MAC\\sfcce.txt");
            l.StandardInput.WriteLine("exit");
            str[8] = l.StandardOutput.ReadToEnd();
            
            //20170704��
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.93 -F D:\\CM_MAC\\sfcce.txt");
            l.StandardInput.WriteLine("exit");
            str[9] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.89 -F D:\\CM_MAC\\sfcce.txt");
            l.StandardInput.WriteLine("exit");
            str[10] = l.StandardOutput.ReadToEnd();


            string middle;
            string[] clientclass=new string[5] ;



            Regex ccreg = new Regex(@"Client Classes: \w+");
            Match ccmatch = ccreg.Match(str[0]);
            string show = null;
            while (ccmatch.Success)
                 {
                    middle = ccmatch.Value.ToString();
                    clientclass = Regex.Split(middle, ": ");
                    show +=clientclass[1]+"\r\n";
                    ccmatch = ccmatch.NextMatch();

                 }

                 textBox12.Text = show;
                 show = null;

                 ccmatch = ccreg.Match(str[1]);
                 while (ccmatch.Success)
                 {
                    middle = ccmatch.Value.ToString();
                    clientclass = Regex.Split(middle, ": ");
                    show += clientclass[1] + "\r\n";
                    ccmatch = ccmatch.NextMatch();

                 }

                 textBox13.Text = show;
                 show = null;

                 ccmatch = ccreg.Match(str[2]);
                 while (ccmatch.Success)
                 {
                     middle = ccmatch.Value.ToString();
                     clientclass = Regex.Split(middle, ": ");
                     show += clientclass[1] + "\r\n";
                     ccmatch = ccmatch.NextMatch();

                 }

                 textBox14.Text = show;
                 show = null;

                 ccmatch = ccreg.Match(str[3]);
                 while (ccmatch.Success)
                 {
                     middle = ccmatch.Value.ToString();
                     clientclass = Regex.Split(middle, ": ");
                     show += clientclass[1] + "\r\n";
                     ccmatch = ccmatch.NextMatch();

                 }

                 textBox15.Text = show;
                 show = null;

                 ccmatch = ccreg.Match(str[4]);
                 while (ccmatch.Success)
                 {
                     middle = ccmatch.Value.ToString();
                     clientclass = Regex.Split(middle, ": ");
                     show += clientclass[1] + "\r\n";
                     ccmatch = ccmatch.NextMatch();

                 }

                 textBox16.Text = show;
                 show = null;

                 ccmatch = ccreg.Match(str[5]);
                 while (ccmatch.Success)
                 {
                     middle = ccmatch.Value.ToString();
                     clientclass = Regex.Split(middle, ": ");
                     show += clientclass[1] + "\r\n";
                     ccmatch = ccmatch.NextMatch();

                 }

                 textBox17.Text = show;
                 show = null;

                 ccmatch = ccreg.Match(str[6]);
                 while (ccmatch.Success)
                 {
                     middle = ccmatch.Value.ToString();
                     clientclass = Regex.Split(middle, ": ");
                     show += clientclass[1] + "\r\n";
                     ccmatch = ccmatch.NextMatch();

                 }

                 textBox18.Text = show;
                 show = null;

                 ccmatch = ccreg.Match(str[7]);
                 while (ccmatch.Success)
                 {
                     middle = ccmatch.Value.ToString();
                     clientclass = Regex.Split(middle, ": ");
                     show += clientclass[1] + "\r\n";
                     ccmatch = ccmatch.NextMatch();

                 }

                 textBox1.Text = show;
                 show = null;

                 ccmatch = ccreg.Match(str[8]);
                 while (ccmatch.Success)
                 {
                     middle = ccmatch.Value.ToString();
                     clientclass = Regex.Split(middle, ": ");
                     show += clientclass[1] + "\r\n";
                     ccmatch = ccmatch.NextMatch();

                 }

                 textBox22.Text = show;
                 show = null;

                 //20170704��
                 ccmatch = ccreg.Match(str[9]);//----------93
                 while (ccmatch.Success)
                 {
                     middle = ccmatch.Value.ToString();
                     clientclass = Regex.Split(middle, ": ");
                     show += clientclass[1] + "\r\n";
                     ccmatch = ccmatch.NextMatch();

                 }

                 textBox19.Text = show;
                 show = null;

                 ccmatch = ccreg.Match(str[10]);//-----------89
                 while (ccmatch.Success)
                 {
                     middle = ccmatch.Value.ToString();
                     clientclass = Regex.Split(middle, ": ");
                     show += clientclass[1] + "\r\n";
                     ccmatch = ccmatch.NextMatch();

                 }

                 textBox20.Text = show;
                 show = null;


                 MessageBox.Show("��ѯ��ϣ�");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string MPath = "C:\\Macfile.txt";
            File.Delete(MPath);
            //string Mac = mac;

            //20170810
            string mac = comboBox5.Text;

            string Comand = null;
            string Comand1 = null;
            //��¡�챦���彻����Ȩ�ж�������hdstb_cm��
            int yon = 0;
            string version = comboBox1.SelectedItem.ToString();
            switch (version)
            {
                case "����": Comand = "modify clientclass CM_disabled\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass CPE_disabled\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                case "ͣ��": Comand = "modify clientclass CM_disabled\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass CPE_disabled\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                case "3CPE��Ȩ": Comand = "modify clientclass istb_CM_3cpe\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass istb_CPE_Private\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                case "ɾ��3CPE��Ȩ": Comand = "modify clientclass istb_CM_3cpe\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass istb_CPE_Private\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                case "ȫ��100M3CPE��Ȩ": Comand = "modify clientclass istb_CM_3cpe_100M\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass istb_CPE_Private\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                case "ɾ��ȫ��100M3CPE��Ȩ": Comand = "modify clientclass istb_CM_3cpe_100M\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass istb_CPE_Private\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                case "Ԥ��Ȩ": Comand = "modify clientclass CM_temp\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                case "ɾ��Ԥ��Ȩ": Comand = "modify clientclass CM_temp\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                //case "���彻����������Ȩ": Comand = "modify clientclass STB_CPE\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                //case "ɾ�����彻����������Ȩ": Comand = "modify clientclass STB_CPE\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                case "���彻����������Ȩ": Comand = "modify clientclass HDSTB_CPE\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";
                    Comand1 = "modify clientclass HDSTB_CM\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; 
                    yon = 1; break;
                case "ɾ�����彻����������Ȩ": Comand = "modify clientclass HDSTB_CPE\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";
                    Comand1 = "modify clientclass HDSTB_CM\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; 
                    yon = 1; break;
                case "wifi_ruckus��Ȩ": Comand = "modify clientclass wifi_ruckus\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";yon=2 ; break;
                case "ɾ��wifi_ruckus��Ȩ": Comand = "modify clientclass wifi_ruckus\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                case "wifi��Ȩ": Comand = "modify clientclass wifi\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; yon = 2; break;
                case "ɾ��wifi��Ȩ": Comand = "modify clientclass wifi\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";break;
                case "Υ���û�����": Comand = "modify clientclass CM_Filter\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                case "Υ���û�ͣ��": Comand = "modify clientclass CM_Filter\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                case "������������Ȩ": Comand = "modify clientclass DJSTB_CPE\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass DJSTB_CM\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                case "ɾ��������������Ȩ": Comand = "modify clientclass DJSTB_CPE\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass DJSTB_CM\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
				//ɾ������Ļ����е㲥��Ȩ*****20170303��ʱ��
				case "ɾ������ĵ㲥��Ȩ": Comand = "modify clientclass HDSTB_CM\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";break;
                case "��ͥ�ײ�100M(D100)": Comand = "modify clientclass STB_D50\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                case "ɾ��D100": Comand = "modify clientclass STB_D50\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;

				
            }

            FileStream MacFile = new FileStream("C:\\macfile.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter MacFileWriter = new StreamWriter(MacFile);
            MacFileWriter.Flush();
            // ʹ��StreamWriter�����ļ���д������
            MacFileWriter.BaseStream.Seek(0, SeekOrigin.Begin);
            // ��richTextBox1�е�����д���ļ�
            MacFileWriter.Write(Comand);
            MacFileWriter.Flush();
            MacFileWriter.Close();

            //�����챦���彻����Ȩ
            FileStream MacFile1 = new FileStream("C:\\Macfile1.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
            MacFileWriter1.Flush();
            // ʹ��StreamWriter�����ļ���д������
            MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);
            // ��richTextBox1�е�����д���ļ�
            MacFileWriter1.Write(Comand1);
            MacFileWriter1.Flush();
            MacFileWriter1.Close();

            //MacFileWriter.Flush();
            //MacFileWriter.Write(Comand);
            //�رմ��ļ�
            //MacFileWriter.Flush();
            //MacFileWriter.Close();

            Process l = new Process();
            l.StartInfo.FileName = "cmd.exe";
            l.StartInfo.UseShellExecute = false;
            l.StartInfo.RedirectStandardInput = true;
            l.StartInfo.RedirectStandardOutput = true;
            l.StartInfo.RedirectStandardError = true;
            l.StartInfo.CreateNoWindow = true;

            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.101 -F C:\\Macfile.txt");
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.103 -F C:\\Macfile.txt");
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.105 -F C:\\Macfile.txt");
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.107 -F C:\\Macfile.txt");
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.109 -F C:\\Macfile.txt");
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.111 -F C:\\Macfile.txt");
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.113 -F C:\\Macfile.txt");
            if (yon == 0)
            {
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.87 -F C:\\Macfile.txt");
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.91 -F C:\\Macfile.txt");
                //20170704
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.89 -F C:\\Macfile.txt");
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.93 -F C:\\Macfile.txt");

            }
            else if(yon==1)  {
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.87 -F C:\\Macfile1.txt");
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.91 -F C:\\Macfile1.txt");
                //20170704
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.89 -F C:\\Macfile1.txt");
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.93 -F C:\\Macfile1.txt");
            }

            else {} //���yon==2,�򲻶�wifiϵ������¡�챦����Ȩ
            l.StandardInput.WriteLine("exit");

            string strRst1 = l.StandardOutput.ReadToEnd();

            strInsert(mac, version, Form2.strName);

            MessageBox.Show("�����ɹ���");

        }

        private void strInsert(string macIns, string eventIns, string operatorIns)
        {
            DateTime dt = DateTime.Now;
            string timeIns = dt.ToString();
            string connOrder2 = "data source='192.168.222.161'; database='grant'; user id = 'root'; password = 'sztop05@MGT'; Charset = utf8;";
            MySqlConnection conn2 = new MySqlConnection(connOrder2);
            conn2.Open();
            string strIns = "insert into BCC_Memo_copy (Mac,Event,Operator,Time) values ('" + macIns + "','" + eventIns + "','" + operatorIns + "','" + timeIns + "');";
            MySqlCommand comIns = new MySqlCommand(strIns, conn2);

            comIns.ExecuteNonQuery();
            conn2.Close();

        }

        private void strInserts(int x, string[] macsInsert, string eventInss, string operatorInss)
        {
            DateTime dt2 = DateTime.Now;
            string timeInss = dt2.ToString();
            string connOrder3 = "data source='192.168.222.161'; database='grant'; user id = 'root'; password = 'sztop05@MGT'; Charset = utf8;";
            MySqlConnection conn3 = new MySqlConnection(connOrder3);
            conn3.Open();
            for (int y = 0; y < x; y++)
            {
                string insert_str = "insert into BCC_Memo_copy (Mac,Event,Operator,Time) values ('" + macsInsert[y] + "','" + eventInss + "','" + operatorInss + "','" + timeInss + "');";
                MySqlCommand comInss = new MySqlCommand(insert_str, conn3);

                comInss.ExecuteNonQuery();
            }
            conn3.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            //20170810
            string mac = comboBox5.Text;

            if (comboBox3.SelectedIndex != -1)
            {
                File.Delete("C:\\istbfile1.txt");
                File.Delete("C:\\istbfile2.txt");
                File.Delete("C:\\fjfsq");
                string command1 = null;
                string command2 = null;
                string location = comboBox3.SelectedItem.ToString();
                int i = 0;
                string jfnum = null;//�������
				string jfnum2 = null;//�챦�������
                for (i = 0; i <56; i++)
                {
                    int bs = string.Compare(location, jfname[i]);
                    if (bs == 0)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                if (i < 37)//����챦���޸�
                {

                    jfnum = num[i];
                }
                else
                {
                    jfnum2 = num2[i];
                }
				
                //if (i < 9)//��Լ�ͥ�ײ�8M��20M��һ��С�޸�
                //{
                //    int s=i+1;
                //    jfnum = "0" + s.ToString();
                //}

                //else

                //{ int s=i+1;
                //    jfnum = s.ToString();
                //}

                string matter = comboBox2.SelectedItem.ToString();
                switch (matter)
                {
                    case "��̬IP��������": command1 = "modify clientclass " + jfglobe[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "��̬IP��˽����": command1 = "modify clientclass " + jfprivate[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    //case "��Ӣ��": command1 = "modify clientclass " + jfjingying[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    //case "���ڰ�": command1 = "modify clientclass " + jf1M[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "�����": command1 = "modify clientclass " + jfjingdian[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    //case "����澲̬": command1 = "modify clientclass " + jfzhuanye[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    //case "��׼��": command1 = "modify clientclass " + jf3M[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    /*case "��׼�澲̬": command1 = "modify clientclass " + jf3Mjingtai[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "�����": command1 = "modify clientclass " + jf6M[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "����澲̬": command1 = "modify clientclass " + jf6Mjingtai[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;

                    case "���ΰ�": command1 = "modify clientclass " + jf8M[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "���ΰ澲̬": command1 = "modify clientclass " + jf8Mjingtai[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
          
                    
                    case "׿Խ��": command1 = "modify clientclass " + jf30M[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "100M": command1 = "modify clientclass " + jf100M[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;*/
                    case "���ڰ�": command1 = "modify clientclass " + jfjiaqi[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "E����1IP": command1 = "modify clientclass " + jf1IP[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "E����2IP": command1 = "modify clientclass " + jf2IP[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "E����8M1IP": command1 = "modify clientclass " + jf4M1IP[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "E����8M2IP": command1 = "modify clientclass " + jf4M2IP[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;

                    case "E����16M1IP": command1 = "modify clientclass " + jf8M1IP[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "E����16M2IP": command1 = "modify clientclass " + jf8M2IP[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                                                          
                    case "E��": command1 = "modify clientclass " + jfCMeconomy[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass " + jfCPEeconomy[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    //case "���彻�����ڰ�": command1 = "modify clientclass istb_CPE_Private\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass istb_CM_1M\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";
                    //    command2 = "modify clientclass " + jfistb1m[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "���彻�������": command1 = "modify clientclass istb_CPE_Private\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass istb_CM_2M\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";
                        command2 = "modify clientclass " + jfistb2m[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    //case "���彻����Ӣ��": command1 = "modify clientclass istb_CPE_Private\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass istb_CM_4M\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";
                    //   command2 = "modify clientclass " + jfistb4m[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;

                    //case "���彻�����ΰ�": command1 = "modify clientclass istb_CPE_Private\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass istb_CM_8M\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";
                    //    command2 = "modify clientclass " + jfistb8m[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                                                      
                    //case "���彻��50M��": command1 = "modify clientclass istb_CPE_Private\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass istb_CM_30M\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";
                    //    command2 = "modify clientclass " + jfistb30m[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;


                    case "��ͨ����6M��": command1 = "modify clientclass " + ltlh6M[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;

                    case "��ͨ����10M��": command1 = "modify clientclass " + ltlh10M[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                   
                    case "4MС����": command1 = "modify clientclass istb_CPE_Private\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass istb_XDK_CM_4M\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";
                        command2 = "modify clientclass " + jfistbxdk[i] + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;

                    case "CM��ͥ�ײ�8M": command1 = "modify clientclass " + "CMTS_"+jfnum+"_CM_FM "+ "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "CM��ͥ�ײ�20M": command1 = "modify clientclass " + "CMTS_" + jfnum + "_CM_FMPLUS " + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;

                    case "�����м�ͥ�ײ�8M": command1 = "modify clientclass istb_CPE_Private\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass istb_CM_FM\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";
                        command2 = "modify clientclass " + "istb_" +jfnum+"_FM_CPE_NAT" +"\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "�����м�ͥ�ײ�20M": command1 = "modify clientclass istb_CPE_Private\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass istb_CM_FMPLUS\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";
                        command2 = "modify clientclass " + "istb_" +jfnum+"_FMPLUS_CPE_NAT"  + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "�챦TV100M": command1 = "modify clientclass istb_CPE_Private\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass Bao_hd_CM_100M\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";
                        command2 = "modify clientclass " + "Bao_hd_" + jfnum2 +"_CPE_NAT"  + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;

                    case "��ͨ����100M": command1 = "modify clientclass " + "CMTS_" + jfnum + "_CM_LTLH100 " + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;

                    case "VPDN": command1 = "modify clientclass SQ_VPDN " + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;

                
                
                }
                if ((matter == "���彻�������") /*|| (matter == "���彻�����ڰ�") || (matter == "���彻�����ΰ�") || (matter == "���彻����Ӣ��") || (matter == "���彻��50M��") */|| (matter == "4MС����") || (matter == "�����м�ͥ�ײ�8M") || (matter == "�����м�ͥ�ײ�20M") || (matter == "�챦TV100M"))
                {
                    FileStream MacFile = new FileStream("C:\\istbfile1.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter MacFileWriter = new StreamWriter(MacFile);
                    MacFileWriter.Flush();
                    // ʹ��StreamWriter�����ļ���д������
                    MacFileWriter.BaseStream.Seek(0, SeekOrigin.Begin);
                    // ��richTextBox1�е�����д���ļ�
                    MacFileWriter.Write(command1);
                    MacFileWriter.Flush();
                    MacFileWriter.Close();
                    FileStream MacFile1 = new FileStream("C:\\istbfile2.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                    MacFileWriter1.Flush();
                    // ʹ��StreamWriter�����ļ���д������
                    MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);
                    // ��richTextBox1�е�����д���ļ�
                    MacFileWriter1.Write(command2);
                    MacFileWriter1.Flush();
                    MacFileWriter1.Close();

                    Process l = new Process();
                    l.StartInfo.FileName = "cmd.exe";
                    l.StartInfo.UseShellExecute = false;
                    l.StartInfo.RedirectStandardInput = true;
                    l.StartInfo.RedirectStandardOutput = true;
                    l.StartInfo.RedirectStandardError = true;
                    l.StartInfo.CreateNoWindow = true;

                    l.Start();
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.101 -F C:\\istbfile1.txt");
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.103 -F C:\\istbfile1.txt");
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.105 -F C:\\istbfile1.txt");
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.107 -F C:\\istbfile1.txt");
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.109 -F C:\\istbfile1.txt");
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.111 -F C:\\istbfile1.txt");
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.113 -F C:\\istbfile1.txt");
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.87 -F C:\\istbfile1.txt");
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.91 -F C:\\istbfile1.txt");
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222." + jfdhcp[i] + " -F C:\\istbfile2.txt");
                    l.StandardInput.WriteLine("exit");

                    string strRst1 = l.StandardOutput.ReadToEnd();
                }
                else
                {
                    FileStream MacFile2 = new FileStream("C:\\fjfsq.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter MacFileWriter2 = new StreamWriter(MacFile2);
                    MacFileWriter2.Flush();
                    // ʹ��StreamWriter�����ļ���д������
                    MacFileWriter2.BaseStream.Seek(0, SeekOrigin.Begin);
                    // ��richTextBox1�е�����д���ļ�
                    MacFileWriter2.Write(command1);
                    MacFileWriter2.Flush();
                    MacFileWriter2.Close();

                    Process l = new Process();
                    l.StartInfo.FileName = "cmd.exe";
                    l.StartInfo.UseShellExecute = false;
                    l.StartInfo.RedirectStandardInput = true;
                    l.StartInfo.RedirectStandardOutput = true;
                    l.StartInfo.RedirectStandardError = true;
                    l.StartInfo.CreateNoWindow = true;

                    l.Start();
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222." + jfdhcp[i] + " -F C:\\fjfsq.txt");
                    l.StandardInput.WriteLine("exit");

                    string strRst1 = l.StandardOutput.ReadToEnd();
                }
                strInsert(mac, "����" + jfname[i] + matter, Form2.strName);
                MessageBox.Show("�����ɹ���");
            }
            else
            {
                MessageBox.Show("��ѡ�������");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //20170810
            string mac = comboBox5.Text;

            if (comboBox3.SelectedIndex != -1)
            {
                File.Delete("C:\\istbfile1.txt");
                File.Delete("C:\\istbfile2.txt");
                File.Delete("C:\\fjfsq");
                string command1 = null;
                string command2 = null;
                string location = comboBox3.SelectedItem.ToString();
                int i = 0;
                string jfnum = null;//�������
				string jfnum2 = null;//�챦�������

                for (i = 0; i < 56; i++)
                {
                    int bs = string.Compare(location, jfname[i]);
                    if (bs == 0)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }

                if (i < 37)//����챦���޸�
                {

                    jfnum = num[i];
                }
                else
                {
                    jfnum2 = num2[i];
                }
                //if (i < 9)
                //{
                //    int s = i + 1;
                //    jfnum = "0" + s.ToString();
                //}

                //else
                //{
                //    int s = i + 1;
                //    jfnum = s.ToString();
                //}
                string matter = comboBox2.SelectedItem.ToString();
                switch (matter)
                {
                    case "��̬IP��������": command1 = "modify clientclass " + jfglobe[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "��̬IP��˽����": command1 = "modify clientclass " + jfprivate[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    //case "��Ӣ��": command1 = "modify clientclass " + jfjingying[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    //case "���ڰ�": command1 = "modify clientclass " + jf1M[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "�����": command1 = "modify clientclass " + jfjingdian[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    //case "����澲̬": command1 = "modify clientclass " + jfzhuanye[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    //case "��׼��": command1 = "modify clientclass " + jf3M[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    //case "��׼�澲̬": command1 = "modify clientclass " + jf3Mjingtai[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    //case "�����": command1 = "modify clientclass " + jf6M[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    //case "����澲̬": command1 = "modify clientclass " + jf6Mjingtai[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;

                    //case "���ΰ�": command1 = "modify clientclass " + jf8M[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    //case "���ΰ澲̬": command1 = "modify clientclass " + jf8Mjingtai[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;   
                    
                    //case "׿Խ��": command1 = "modify clientclass " + jf30M[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    //case "100M": command1 = "modify clientclass " + jf100M[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "���ڰ�": command1 = "modify clientclass " + jfjiaqi[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "E����1IP": command1 = "modify clientclass " + jf1IP[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "E����2IP": command1 = "modify clientclass " + jf2IP[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "E����8M1IP": command1 = "modify clientclass " + jf4M1IP[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "E����8M2IP": command1 = "modify clientclass " + jf4M2IP[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "E��": command1 = "modify clientclass " + jfCMeconomy[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass " + jfCPEeconomy[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    //case "���彻�����ڰ�": command1 = "modify clientclass istb_CPE_Private\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass istb_CM_1M\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";
                    //   command2 = "modify clientclass " + jfistb1m[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "���彻�������": command1 = "modify clientclass istb_CPE_Private\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass istb_CM_2M\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";
                        command2 = "modify clientclass " + jfistb2m[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    //case "���彻����Ӣ��": command1 = "modify clientclass istb_CPE_Private\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass istb_CM_4M\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";
                    //    command2 = "modify clientclass " + jfistb4m[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    //case "���彻�����ΰ�": command1 = "modify clientclass istb_CPE_Private\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass istb_CM_8M\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";
                    //    command2 = "modify clientclass " + jfistb8m[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;

                    case "E����16M1IP": command1 = "modify clientclass " + jf8M1IP[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "E����16M2IP": command1 = "modify clientclass " + jf8M2IP[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                  
                    
                    //case "���彻��50M��": command1 = "modify clientclass istb_CPE_Private\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass istb_CM_30M\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";
                    //   command2 = "modify clientclass " + jfistb30m[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;


                    case "��ͨ����6M��": command1 = "modify clientclass " + ltlh6M[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;

                    case "��ͨ����10M��": command1 = "modify clientclass " + ltlh10M[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "4MС����": command1 = "modify clientclass istb_CPE_Private\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass istb_XDK_CM_4M\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";
                        command2 = "modify clientclass " + jfistbxdk[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "CM��ͥ�ײ�8M": command1 = "modify clientclass " + "CMTS_" + jfnum + "_CM_FM " + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "CM��ͥ�ײ�20M": command1 = "modify clientclass " + "CMTS_" + jfnum + "_CM_FMPLUS " + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;

                    case "�����м�ͥ�ײ�8M": command1 = "modify clientclass istb_CPE_Private\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass istb_CM_FM\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";
                        command2 = "modify clientclass " + "istb_" + jfnum + "_FM_CPE_NAT" + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "�����м�ͥ�ײ�20M": command1 = "modify clientclass istb_CPE_Private\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass istb_CM_FMPLUS\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";
                        command2 = "modify clientclass " + "istb_" + jfnum + "_FMPLUS_CPE_NAT" + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
                    case "�챦TV100M": command1 = "modify clientclass istb_CPE_Private\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "modify clientclass Bao_hd_CM_100M\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit";
                        command2 = "modify clientclass " + "Bao_hd_" + jfnum2 +"_CPE_NAT"  + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;

                    case "��ͨ����100M": command1 = "modify clientclass " + "CMTS_" + jfnum + "_CM_LTLH100 " + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;

                    case "VPDN": command1 = "modify clientclass SQ_VPDN " + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n" + "exit"; break;
   
                
                }
                if ((matter == "���彻�������") /*|| (matter == "���彻�����ڰ�") || (matter == "���彻�����ΰ�") || (matter == "���彻����Ӣ��") || (matter == "���彻��50M��") */|| (matter == "4MС����") || (matter == "�����м�ͥ�ײ�8M") || (matter == "�����м�ͥ�ײ�20M") || (matter == "�챦TV100M"))
                {
                    FileStream MacFile = new FileStream("C:\\istbfile1.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter MacFileWriter = new StreamWriter(MacFile);
                    MacFileWriter.Flush();
                    // ʹ��StreamWriter�����ļ���д������
                    MacFileWriter.BaseStream.Seek(0, SeekOrigin.Begin);
                    // ��richTextBox1�е�����д���ļ�
                    MacFileWriter.Write(command1);
                    MacFileWriter.Flush();
                    MacFileWriter.Close();
                    FileStream MacFile1 = new FileStream("C:\\istbfile2.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                    MacFileWriter1.Flush();
                    // ʹ��StreamWriter�����ļ���д������
                    MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);
                    // ��richTextBox1�е�����д���ļ�
                    MacFileWriter1.Write(command2);
                    MacFileWriter1.Flush();
                    MacFileWriter1.Close();

                    Process l = new Process();
                    l.StartInfo.FileName = "cmd.exe";
                    l.StartInfo.UseShellExecute = false;
                    l.StartInfo.RedirectStandardInput = true;
                    l.StartInfo.RedirectStandardOutput = true;
                    l.StartInfo.RedirectStandardError = true;
                    l.StartInfo.CreateNoWindow = true;

                    l.Start();
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.101 -F C:\\istbfile1.txt");
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.103 -F C:\\istbfile1.txt");
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.105 -F C:\\istbfile1.txt");
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.107 -F C:\\istbfile1.txt");
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.109 -F C:\\istbfile1.txt");
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.111 -F C:\\istbfile1.txt");
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.113 -F C:\\istbfile1.txt");
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.87 -F C:\\istbfile1.txt");
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.91 -F C:\\istbfile1.txt");
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222." + jfdhcp[i] + " -F C:\\istbfile2.txt");
                    l.StandardInput.WriteLine("exit");

                    string strRst1 = l.StandardOutput.ReadToEnd();
                }
                else
                {
                    FileStream MacFile2 = new FileStream("C:\\fjfsq.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter MacFileWriter2 = new StreamWriter(MacFile2);
                    MacFileWriter2.Flush();
                    // ʹ��StreamWriter�����ļ���д������
                    MacFileWriter2.BaseStream.Seek(0, SeekOrigin.Begin);
                    // ��richTextBox1�е�����д���ļ�
                    MacFileWriter2.Write(command1);
                    MacFileWriter2.Flush();
                    MacFileWriter2.Close();

                    Process l = new Process();
                    l.StartInfo.FileName = "cmd.exe";
                    l.StartInfo.UseShellExecute = false;
                    l.StartInfo.RedirectStandardInput = true;
                    l.StartInfo.RedirectStandardOutput = true;
                    l.StartInfo.RedirectStandardError = true;
                    l.StartInfo.CreateNoWindow = true;

                    l.Start();
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222." + jfdhcp[i] + " -F C:\\fjfsq.txt");
                    l.StandardInput.WriteLine("exit");

                    string strRst1 = l.StandardOutput.ReadToEnd();
                }
                strInsert(mac, "ɾ��" + jfname[i] + matter, Form2.strName);
                MessageBox.Show("�����ɹ���");
            }
            else
            {
                MessageBox.Show("��ѡ�������");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                System.IO.FileInfo ObjTmp = new System.IO.FileInfo(openFileDialog1.FileName);
                textBox2.Text = openFileDialog1.FileName;
                MacList = openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string MLPath = "C:\\MacListfile.txt";
            File.Delete(MLPath);
            string MacTxt = "";
            string MacTxt1 ="";
            int ins = 0;
            //��¡�챦���彻����Ȩ�ж�
            int yon2 = 0;
            int yon3 = 0;
            string[] strsInss = new string[200000];
            FileStream MacListFileR = new FileStream(MacList, FileMode.Open, FileAccess.Read);
            StreamReader MacListFileReader = new StreamReader(MacListFileR);
            MacListFileReader.BaseStream.Seek(0, SeekOrigin.Begin);
            string strLine = MacListFileReader.ReadLine();
            string ListVersion = comboBox4.SelectedItem.ToString();
            switch (ListVersion)
            {
                case "��������": while (strLine != null)
                    {
                        MacTxt += "modify clientclass CM_disabled\r\n" + "delete clientclassentry " + strLine + "\r\n" + 

"save\r\n" + "exit\r\n" + "modify clientclass CPE_disabled\r\n" + "delete clientclassentry " + strLine + "\r\n" + 

"save\r\n" + "exit\r\n";
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;

                case "����ͣ��": while (strLine != null)
                    {
                        MacTxt += "modify clientclass CM_disabled\r\n" + "add clientclassentry " + strLine + "\r\n" + 

"save\r\n" + "exit\r\n" + "modify clientclass CPE_disabled\r\n" + "add clientclassentry " + strLine + "\r\n" + "save\r\n" + 

"exit\r\n";
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;

                case "����Ԥ��Ȩ": while (strLine != null)
                    {
                        MacTxt += "modify clientclass CM_temp\r\n" + "add clientclassentry " + strLine + "\r\n" + 

"save\r\n" + "exit\r\n";
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;

                case "����ɾ��Ԥ��Ȩ": while (strLine != null)
                    {
                        MacTxt += "modify clientclass CM_temp\r\n" + "delete clientclassentry " + strLine + "\r\n" + 

"save\r\n" + "exit\r\n";
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;

               

                case "�������彻����������Ȩ": while (strLine != null)
                    {
                        MacTxt += "modify clientclass HDSTB_CPE\r\n" + "add clientclassentry " + strLine + "\r\n" + 

"save\r\n" + "exit\r\n";
                        //MacTxt1 += "modify clientclass HDSTB_CM\r\n" + "add clientclassentry " + strLine + "\r\n" +

//"save\r\n" + "exit\r\n";
                        //yon2 = 1;
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;

                case "����ɾ�����彻����������Ȩ": while (strLine != null)
                    {
                        MacTxt += "modify clientclass HDSTB_CPE\r\n" + "delete clientclassentry " + strLine + "\r\n" + 

"save\r\n" + "exit\r\n";
                        //MacTxt1 += "modify clientclass HDSTB_CM\r\n" + "delete clientclassentry " + strLine + "\r\n" +

//"save\r\n" + "exit\r\n";
                        //yon2 = 1;
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;

                case "����D100": while (strLine != null)
                    {
                        MacTxt += "modify clientclass STB_D50\r\n" + "add clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n";
                        //MacTxt1 += "modify clientclass HDSTB_CM\r\n" + "add clientclassentry " + strLine + "\r\n" +

                        //"save\r\n" + "exit\r\n";
                        //yon2 = 1;
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;

                case "����ɾ��D100": while (strLine != null)
                    {
                        MacTxt += "modify clientclass STB_D50\r\n" + "delete clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n";
                        //MacTxt1 += "modify clientclass HDSTB_CM\r\n" + "delete clientclassentry " + strLine + "\r\n" +

                        //"save\r\n" + "exit\r\n";
                        //yon2 = 1;
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;

                case "�����챦�����������Ȩ": while (strLine != null)
                    {
                        MacTxt += "modify clientclass HDSTB_CM\r\n" + "add clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n";
                        //MacTxt1 += "modify clientclass HDSTB_CM\r\n" + "add clientclassentry " + strLine + "\r\n" +

                        //"save\r\n" + "exit\r\n";
                       yon2 = 1;
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;

                case "����ɾ���챦�����������Ȩ": while (strLine != null)
                    {
                        MacTxt += "modify clientclass HDSTB_CM\r\n" + "delete clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n";
                        //MacTxt1 += "modify clientclass HDSTB_CM\r\n" + "delete clientclassentry " + strLine + "\r\n" +

                        //"save\r\n" + "exit\r\n";
                        yon2 = 1;
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;
                case "������¡�����������Ȩ": while (strLine != null)
                    {
                        MacTxt += "modify clientclass HDSTB_CM\r\n" + "add clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n";
                        //MacTxt1 += "modify clientclass HDSTB_CM\r\n" + "add clientclassentry " + strLine + "\r\n" +

                        //"save\r\n" + "exit\r\n";
                        yon3 = 1;
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;

                case "����ɾ����¡�����������Ȩ": while (strLine != null)
                    {
                        MacTxt += "modify clientclass HDSTB_CM\r\n" + "delete clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n";
                        //MacTxt1 += "modify clientclass HDSTB_CM\r\n" + "delete clientclassentry " + strLine + "\r\n" +

                        //"save\r\n" + "exit\r\n";
                        yon3 = 1;
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;

                case "����������������Ȩ": while (strLine != null)
                    {
                        MacTxt += "modify clientclass DJSTB_CM\r\n" + "add clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n" + "modify clientclass DJSTB_CPE\r\n" + "add clientclassentry " + strLine + "\r\n" + 

"save\r\n" + "exit\r\n";
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;

                case "����ɾ��������������Ȩ": while (strLine != null)
                    {
                        MacTxt += "modify clientclass DJSTB_CM\r\n" + "delete clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n" + "modify clientclass DJSTB_CPE\r\n" + "delete clientclassentry " + strLine + "\r\n" + 

"save\r\n" + "exit\r\n";
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;

                case "����IP������������Ȩ": while (strLine != null)
                    {
                        MacTxt += "modify clientclass IPDJSTB_CM\r\n" + "add clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n" + "modify clientclass IPDJSTB_CPE\r\n" + "add clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n";
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;

                case "����ɾ��IP������������Ȩ": while (strLine != null)
                    {
                        MacTxt += "modify clientclass IPDJSTB_CM\r\n" + "delete clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n" + "modify clientclass IPDJSTB_CPE\r\n" + "delete clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n";
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;

                case "����wifi_ruckus��Ȩ": while (strLine != null)
                    {
                        MacTxt += "modify clientclass wifi_ruckus\r\n" + "add clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n";
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;

                case "����ɾ��wifi_ruckus��Ȩ": while (strLine != null)
                    {
                        MacTxt += "modify clientclass wifi_ruckus\r\n" + "delete clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n";
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;

                case "����Wifi��Ȩ": while (strLine != null)
                    {
                        MacTxt += "modify clientclass Wifi\r\n" + "add clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n";
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;

                case "����ɾ��Wifi��Ȩ": while (strLine != null)
                    {
                        MacTxt += "modify clientclass Wifi\r\n" + "delete clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n";
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;

                case "����3CPE��Ȩ": while (strLine != null)
                    {
                        MacTxt += "modify clientclass istb_CM_3cpe\r\n" + "add clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n" + "modify clientclass istb_CPE_Private\r\n" + "add clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n";
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;
                
                case "����ɾ��3CPE��Ȩ": while (strLine != null)
                    {
                        MacTxt += "modify clientclass istb_CM_3cpe\r\n" + "delete clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n" + "modify clientclass istb_CPE_Private\r\n" + "delete clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n";
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;

                case "����ȫ��100M3CPE��Ȩ": while (strLine != null)
                    {
                        MacTxt += "modify clientclass istb_CM_3cpe_100M\r\n" + "add clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n" + "modify clientclass istb_CPE_Private\r\n" + "add clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n";
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;

                case "����ɾ��ȫ��100M3CPE��Ȩ": while (strLine != null)
                    {
                        MacTxt += "modify clientclass istb_CM_3cpe_100M\r\n" + "delete clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n" + "modify clientclass istb_CPE_Private\r\n" + "delete clientclassentry " + strLine + "\r\n" +

"save\r\n" + "exit\r\n";
                        strsInss[ins] = strLine;
                        ins++;
                        strLine = MacListFileReader.ReadLine();
                    }
                    break;
            }

            FileStream MacListFileW = new FileStream("C:\\MacListfile.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter MacListFileWriter = new StreamWriter(MacListFileW);
            MacListFileWriter.Flush();
            // ʹ��StreamWriter�����ļ���д������
            MacListFileWriter.BaseStream.Seek(0, SeekOrigin.Begin);
            // ��richTextBox1�е�����д���ļ�
            MacTxt += "exit\r\n";
            MacListFileWriter.Write(MacTxt);
            //�رմ��ļ�;
            MacListFileWriter.Flush();
            MacListFileWriter.Close();

            FileStream MacListFileW1 = new FileStream("C:\\MacListfile1.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter MacListFileWriter1 = new StreamWriter(MacListFileW1);
            MacListFileWriter1.Flush();
            // ʹ��StreamWriter�����ļ���д������
            MacListFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);
            // ��richTextBox1�е�����д���ļ�
            MacTxt1 += "exit\r\n";
            MacListFileWriter1.Write(MacTxt1);
            //�رմ��ļ�;
            MacListFileWriter1.Flush();
            MacListFileWriter1.Close();



            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;

            p.Start();
            if ((yon2 == 1) ||(yon3 == 1))//���yon2==1����yon3==1,���mac�б������¡�챦���彻����Ȩ
            {
                if (yon2 == 1) //���yon2==1�����mac�����챦�������Ȩ
                {
                    p.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.87 -F C:\\MacListfile.txt");
                    //20170704
                    p.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.89 -F C:\\MacListfile.txt");

                }

                else//���yon3==1�����mac������¡�������Ȩ
                {
                    p.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.91 -F C:\\MacListfile.txt");
                    //20170704
                    p.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.93 -F C:\\MacListfile.txt");
                }
            }
            else
            {
                p.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.101 -F C:\\MacListfile.txt");
                p.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.103 -F C:\\MacListfile.txt");
                p.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.105 -F C:\\MacListfile.txt");
                p.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.107 -F C:\\MacListfile.txt");
                p.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.109 -F C:\\MacListfile.txt");
                p.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.111 -F C:\\MacListfile.txt");
                p.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.113 -F C:\\MacListfile.txt");
            }

                p.StandardInput.WriteLine("exit");

            string strRst = p.StandardOutput.ReadToEnd();

            strInserts(ins, strsInss, ListVersion, Form2.strName);

            MessageBox.Show("�����ɹ���");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int X0 = 0;
            int X1 = 0;
            int X2 = 0;
            int X3 = 0;
            int X4 = 0;
            int X5 = 0;
            int X6 = 0;
            int X7 = 0;
            string[] str = new string[10];
            string key = comboBox8.SelectedItem.ToString();
            string Command = "";
            FileStream CCListFileR = new FileStream(MacList, FileMode.Open, FileAccess.Read);
            StreamReader CCListFileReader = new StreamReader(CCListFileR);
            CCListFileReader.BaseStream.Seek(0, SeekOrigin.Begin);
            string strLine = CCListFileReader.ReadLine();

            while (strLine != null)
            {
                Command += strLine + "\r\n" + "list search clientclassentry hardwareaddress " + strLine + "\r\n" + "list search clientclassentry remoteID " + strLine + "\r\n";
                strLine = CCListFileReader.ReadLine();
                X0++;
            }

            Command = Command + "exit\r\n";

            FileStream MacFile = new FileStream("D:\\CM_MAC\\CheckU.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter MacFileWriter = new StreamWriter(MacFile);
            MacFileWriter.Flush();
            // ʹ��StreamWriter�����ļ���д������
            MacFileWriter.BaseStream.Seek(0, SeekOrigin.Begin);
            // ��richTextBox1�е�����д���ļ�
            MacFileWriter.Write(Command);
            MacFileWriter.Flush();
            MacFileWriter.Close();

            Process l = new Process();
            l.StartInfo.FileName = "cmd.exe";
            l.StartInfo.UseShellExecute = false;
            l.StartInfo.RedirectStandardInput = true;
            l.StartInfo.RedirectStandardOutput = true;
            l.StartInfo.RedirectStandardError = true;
            l.StartInfo.CreateNoWindow = true;

            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.101 -F D:\\CM_MAC\\CheckU.txt");
            l.StandardInput.WriteLine("exit");
            str[0] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.103 -F D:\\CM_MAC\\CheckU.txt");
            l.StandardInput.WriteLine("exit");
            str[1] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.105 -F D:\\CM_MAC\\CheckU.txt");
            l.StandardInput.WriteLine("exit");
            str[2] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.107 -F D:\\CM_MAC\\CheckU.txt");
            l.StandardInput.WriteLine("exit");
            str[3] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.109 -F D:\\CM_MAC\\CheckU.txt");
            l.StandardInput.WriteLine("exit");
            str[4] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.111 -F D:\\CM_MAC\\CheckU.txt");
            l.StandardInput.WriteLine("exit");
            str[5] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.113 -F D:\\CM_MAC\\CheckU.txt");
            l.StandardInput.WriteLine("exit");
            str[6] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.87 -F D:\\CM_MAC\\CheckU.txt");
            l.StandardInput.WriteLine("exit");
            str[7] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.91 -F D:\\CM_MAC\\CheckU.txt");
            l.StandardInput.WriteLine("exit");
            str[8] = l.StandardOutput.ReadToEnd();

            string middle;
            string[] clientclass = new string[5];
            textBox3.Text = "mac����Ϊ" + X0.ToString();


            Regex ccreg = new Regex(@"Client Classes: \w+");
            //Regex macreg = new Regex(@"recognize ............");




            Match ccmatch = ccreg.Match(str[0]);
            while (ccmatch.Success)
            {
                middle = ccmatch.Value.ToString();
                clientclass = Regex.Split(middle, ": ");
                if (clientclass[1] == key)
                {
                    X1++;
                }
                ccmatch = ccmatch.NextMatch();

            }
            textBox4.Text = key + "�����ļ�����Ϊ" + X1.ToString();

            ccmatch = ccreg.Match(str[1]);
            while (ccmatch.Success)
            {
                middle = ccmatch.Value.ToString();
                clientclass = Regex.Split(middle, ": ");
                if (clientclass[1] == key)
                {
                    X2++;
                }
                ccmatch = ccmatch.NextMatch();

            }
            textBox6.Text = key + "�����ļ�����Ϊ" + X2.ToString();

            ccmatch = ccreg.Match(str[2]);
            while (ccmatch.Success)
            {
                middle = ccmatch.Value.ToString();
                clientclass = Regex.Split(middle, ": ");
                if (clientclass[1] == key)
                {
                    X3++;
                }
                ccmatch = ccmatch.NextMatch();

            }
            textBox8.Text = key + "�����ļ�����Ϊ" + X3.ToString();

            ccmatch = ccreg.Match(str[3]);
            while (ccmatch.Success)
            {
                middle = ccmatch.Value.ToString();
                clientclass = Regex.Split(middle, ": ");
                if (clientclass[1] == key)
                {
                    X4++;
                }
                ccmatch = ccmatch.NextMatch();

            }
            textBox9.Text = key + "�����ļ�����Ϊ" + X4.ToString();

            ccmatch = ccreg.Match(str[4]);
            while (ccmatch.Success)
            {
                middle = ccmatch.Value.ToString();
                clientclass = Regex.Split(middle, ": ");
                if (clientclass[1] == key)
                {
                    X5++;
                }
                ccmatch = ccmatch.NextMatch();

            }
            textBox10.Text = key + "�����ļ�����Ϊ" + X5.ToString();

            ccmatch = ccreg.Match(str[5]);
            while (ccmatch.Success)
            {
                middle = ccmatch.Value.ToString();
                clientclass = Regex.Split(middle, ": ");
                if (clientclass[1] == key)
                {
                    X6++;
                }
                ccmatch = ccmatch.NextMatch();

            }
            textBox5.Text = key + "�����ļ�����Ϊ" + X6.ToString();

            ccmatch = ccreg.Match(str[6]);
            while (ccmatch.Success)
            {
                middle = ccmatch.Value.ToString();
                clientclass = Regex.Split(middle, ": ");
                if (clientclass[1] == key)
                {
                    X7++;
                }
                ccmatch = ccmatch.NextMatch();

            }
            textBox7.Text = key + "�����ļ�����Ϊ" + X7.ToString();

            MessageBox.Show("Success��");
        }


        //ClearCPE��������������DHCP������
        string[] jffname = new string[] { "�����", "��ɽ", "�ʸ�", "��԰", "÷��", "������", "�ٻ�", "������", "����", "����", "������", "ɳͷ��", "��ó", "��Դ��", "���ȳ�", "����", "����", "����", "�Ƽ�԰", "����", "����", "����", "��ڱ�", "������", "԰��԰", "��ó��", "����", "����", "����", "������", "���ɽ", "���㶫", "�ϲ���", "����", "������", "����", "����" };
        string[] jffdhcp = new string[] { "107", "113", "101", "111", "111", "105", "113", "103", "101", "105", "109", "107", "103", "109", "113", "107", "107", "109", "111", "107", "107", "107", "107", "101", "103", "101", "105", "109", "109", "103",  "101", "105", "109", "111", "109", "105", "103" };
        string[, ] addpool = new string[, ] {
{"222.248.44.1", "222.248.47.254", "8", "8" }, { "116.77.12.1", "116.77.31.1", "8", "8"  }, { "222.125.14.1", "222.125.63.1", "8", "8"  }, { "116.76.47.1", "9", "8", "8" }, 
{ "115.44.183.1", "9", "222.248.156.1", "222.248.159.1" }, 
{ "222.248.220.1", "222.248.223.1", "8", "8" },
{ "222.248.110.1", "222.248.111.1", "8", "8" },
{ "222.125.221.1", "222.125.255.1", "8", "8" }, 
{ "222.125.94.1", "222.125.95.1", "8", "8" }, 
{ "116.76.223.1", "9", "8", "8" }, 
{ "116.76.159.1", "9", "8", "8" },
{ "116.76.252.201", "9", "8", "8" }, 
{ "116.76.191.1", "9", "8", "8" }, 
{"116.76.63.1", "9", "116.77.174.1", "116.77.175.1" }, 
{ "115.44.191.1", "9", "116.77.156.1", "116.77.159.1" }, 
{ "222.125.189.1", "222.125.191.1", "8", "8" }, 
{ "222.125.111.1", "9", "116.76.238.1", "116.76.239.1" }, 
{ "116.76.94.1", "116.76.95.1", "8", "8" }, 
{ "111.222.94.1", "111.222.95.1", "8", "8" }, 
{ "116.77.213.1", "116.77.215.1", "116.76.125.1", "116.76.127.1" },
{ "111.222.108.1", "111.222.111.1", "8", "8" },
{ "222.125.119.1", "9", "8", "8" }, 
{ "116.76.247.1", "116.77.243.1", "8", "8" }, 
{ "116.77.87.1", "9", "8", "8" }, 
{ "116.77.63.1", "9", "8", "8" }, 
{ "116.76.254.121", "9", "8", "8" },
{ "222.125.103.1", "9", "8", "8" },
{ "219.232.183.201", "9", "8", "8" },
{ "116.77.179.1", "9", "8", "8" },
{ "222.248.127.1", "9", "8", "8" },
{ "116.77.183.1", "9", "8", "8" },
{ "115.45.217.1", "115.45.223.1", "8", "8" }, { "222.125.159.1", "9", "8", "8" }, { "116.76.30.1", "116.76.31.1", "222.248.191.1", "9" }, { "111.222.30.1", "111.222.63.1", "8", "8" }, { "111.222.78.1", "111.222.79.1", "8", "8" }, { "111.222.156.1", "111.222.159.1", "8", "8" }};


        

        // CC controller
        string MaccList = null;
        string cccdhcp = null;

       

        private void button10_Click(object sender, EventArgs e)
        {
            string ccpath = "C:\\CCfile.txt";
            File.Delete(ccpath);
            comboBox7.Items.Clear();
            cccdhcp = comboBox6.SelectedItem.ToString();
            string cccommand1 = "list clientclass" + "\r\n";
            FileStream CCFile = new FileStream("C:\\CCfile.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter CCFileWriter = new StreamWriter(CCFile);
            CCFileWriter.Flush();
            // ʹ��StreamWriter�����ļ���д������
            CCFileWriter.BaseStream.Seek(0, SeekOrigin.Begin);
            // ��richTextBox1�е�����д���ļ�
            CCFileWriter.Write(cccommand1);
            CCFileWriter.Flush();
            CCFileWriter.Close();

            Process cc = new Process();
            cc.StartInfo.FileName = "cmd.exe";
            cc.StartInfo.UseShellExecute = false;
            cc.StartInfo.RedirectStandardInput = true;
            cc.StartInfo.RedirectStandardOutput = true;
            cc.StartInfo.RedirectStandardError = true;
            cc.StartInfo.CreateNoWindow = true;

            cc.Start();
            cc.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222." + cccdhcp + " -F C:\\CCfile.txt");
            cc.StandardInput.WriteLine("exit");
            string strRstcc = cc.StandardOutput.ReadToEnd();

            string ccname1 = null;
            //����������ʽ string test1 = null;
            string[] ccname = null;
            Regex ccreg = new Regex("NAME: " + @"\w+");
            Match ccmatch = ccreg.Match(strRstcc);
            //����������ʽ test1 = ccmatch.Value.ToString();
            //����������ʽ string test2 = null;

            while (ccmatch.Success)
            {
                ccname1 = ccmatch.Value.ToString();
                ccname = Regex.Split(ccname1, " ");
                comboBox7.Items.Add(ccname[1]);
                ccmatch = ccmatch.NextMatch();

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog(this) == DialogResult.OK)
            {
                System.IO.FileInfo ObjTmp = new System.IO.FileInfo(openFileDialog2.FileName);
                textBox21.Text = openFileDialog2.FileName;
                MacList = openFileDialog2.FileName;
            }  
        }



        private void strInsertss(int x, string[] macsInsert, string eventInss, string operatorInss)
        {
            DateTime dt3 = DateTime.Now;
            string timeInss = dt3.ToString();
            string connOrder4 = "data source='192.168.222.161'; database='grant'; user id = 'root'; password = 'sztop05@MGT'; Charset = utf8;";
            MySqlConnection conn4 = new MySqlConnection(connOrder4);
            conn4.Open();
            for (int y = 0; y < x; y++)
            {
                string insert_str = "insert into BCC_Memo_copy (Mac,Event,Operator,Time) values ('" + macsInsert[y] + "','" + eventInss + "','" + operatorInss + "','" + timeInss + "');";
                MySqlCommand comInss = new MySqlCommand(insert_str, conn4);
   
                comInss.ExecuteNonQuery();
            }
            conn4.Close();
        }


        private void button12_Click(object sender, EventArgs e)
        {
            string CCPath = "C:\\CCListfile.txt";
            File.Delete(CCPath);
            string Command = "";
            int ins = 0;
            string[] strsInss = new string[200000];
            FileStream CCListFileR = new FileStream(MacList, FileMode.Open, FileAccess.Read);
            StreamReader CCListFileReader = new StreamReader(CCListFileR);
            CCListFileReader.BaseStream.Seek(0, SeekOrigin.Begin);
            string strLine = CCListFileReader.ReadLine();

            string cccdhcp = comboBox6.SelectedItem.ToString();

            string ccInfo = comboBox7.SelectedItem.ToString();
            while (strLine != null)
            {

                Command += "modify clientclass " + ccInfo  + "\r\n" + "add clientclassentry " + strLine + "\r\n" + "save\r\n" + "exit\r\n";
                strsInss[ins] = strLine;
                ins++;
                strLine = CCListFileReader.ReadLine();
            }

            FileStream CCListFileW = new FileStream("C:\\CCListfile.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter CCListFileWriter = new StreamWriter(CCListFileW);
            CCListFileWriter.Flush();
            // ʹ��StreamWriter�����ļ���д������
            CCListFileWriter.BaseStream.Seek(0, SeekOrigin.Begin);
            // ��richTextBox1�е�����д���ļ�
            Command += "exit\r\n";
            CCListFileWriter.Write(Command);
            //�رմ��ļ�;
            CCListFileWriter.Flush();
            CCListFileWriter.Close();

            Process CC = new Process();
            CC.StartInfo.FileName = "cmd.exe";
            CC.StartInfo.UseShellExecute = false;
            CC.StartInfo.RedirectStandardInput = true;
            CC.StartInfo.RedirectStandardOutput = true;
            CC.StartInfo.RedirectStandardError = true;
            CC.StartInfo.CreateNoWindow = true;

            CC.Start();
            CC.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222."+cccdhcp+" -F C:\\CCListfile.txt");
            CC.StandardInput.WriteLine("exit");

            string strRst = CC.StandardOutput.ReadToEnd();

            strInsertss(ins, strsInss, "���DHCP" + cccdhcp + "��Ȩ" + ccInfo, Form2.strName);

            MessageBox.Show("�����ɹ���");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string CCPath = "C:\\CCListfile.txt";
            File.Delete(CCPath);
            string Command = "";
            int ins = 0;
            string[] strsInss = new string[200000];
            FileStream CCListFileR = new FileStream(MacList, FileMode.Open, FileAccess.Read);
            StreamReader CCListFileReader = new StreamReader(CCListFileR);
            CCListFileReader.BaseStream.Seek(0, SeekOrigin.Begin);
            string strLine = CCListFileReader.ReadLine();

            string cccdhcp = comboBox6.SelectedItem.ToString();
            string ccInfo = comboBox7.SelectedItem.ToString();
            while (strLine != null)
            {
                Command += "modify clientclass " + ccInfo + "\r\n" + "delete clientclassentry " + strLine + "\r\n" + "save\r\n" + "exit\r\n";

                strsInss[ins] = strLine;
                ins++;
                strLine = CCListFileReader.ReadLine();
            }

            FileStream CCListFileW = new FileStream("C:\\CCListfile.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter CCListFileWriter = new StreamWriter(CCListFileW);
            CCListFileWriter.Flush();
            // ʹ��StreamWriter�����ļ���д������
            CCListFileWriter.BaseStream.Seek(0, SeekOrigin.Begin);
            // ��richTextBox1�е�����д���ļ�
            Command += "exit\r\n";
            CCListFileWriter.Write(Command);
            //�رմ��ļ�;
            CCListFileWriter.Flush();
            CCListFileWriter.Close();

            Process CC = new Process();
            CC.StartInfo.FileName = "cmd.exe";
            CC.StartInfo.UseShellExecute = false;
            CC.StartInfo.RedirectStandardInput = true;
            CC.StartInfo.RedirectStandardOutput = true;
            CC.StartInfo.RedirectStandardError = true;
            CC.StartInfo.CreateNoWindow = true;

            CC.Start();
            CC.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222." + cccdhcp + " -F C:\\CCListfile.txt");
            CC.StandardInput.WriteLine("exit");

            string strRst = CC.StandardOutput.ReadToEnd();

            strInsertss(ins, strsInss, "ɾ��DHCP" + cccdhcp + "��Ȩ" + ccInfo, Form2.strName);
            MessageBox.Show("�����ɹ���");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string MRePath = "C:\\MacReFile.txt";
            File.Delete(MRePath);
            //20170810
            string mac = comboBox5.Text;
            string CommandRe = "show lease " + mac + "\r\n" + "exit\r\n";

            FileStream MacReFile = new FileStream("C:\\MacReFile.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter MacReFileWriter = new StreamWriter(MacReFile);
            MacReFileWriter.Flush();
            MacReFileWriter.BaseStream.Seek(0, SeekOrigin.Begin);
            MacReFileWriter.Write(CommandRe);
            MacReFileWriter.Flush();
            MacReFileWriter.Close();

            Process R = new Process();
            R.StartInfo.FileName = "cmd.exe";
            R.StartInfo.UseShellExecute = false;
            R.StartInfo.RedirectStandardInput = true;
            R.StartInfo.RedirectStandardOutput = true;
            R.StartInfo.RedirectStandardError = true;
            R.StartInfo.CreateNoWindow = true;

            R.Start();
            R.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.101 -F C:\\MacReFile.txt");
            R.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.103 -F C:\\MacReFile.txt");
            R.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.105 -F C:\\MacReFile.txt");
            R.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.107 -F C:\\MacReFile.txt");
            R.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.109 -F C:\\MacReFile.txt");
            R.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.111 -F C:\\MacReFile.txt");
            R.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.113 -F C:\\MacReFile.txt");
            R.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.87 -F C:\\MacReFile.txt");
            R.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.89 -F C:\\MacReFile.txt");
            R.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.91 -F C:\\MacReFile.txt");
            R.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.93 -F C:\\MacReFile.txt");
            R.StandardInput.WriteLine("exit");

            string strRst2 = R.StandardOutput.ReadToEnd();
            FileStream ReBootFile = new FileStream("C:\\ReBootFile.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter ReBootFileWriter = new StreamWriter(ReBootFile);
            ReBootFileWriter.Flush();//ˢ�»�����
            // ʹ��StreamWriter�����ļ���д������
            ReBootFileWriter.BaseStream.Seek(0, SeekOrigin.Begin);
            // ��richTextBox1�е�����д���ļ�
            ReBootFileWriter.Write(strRst2);
            ReBootFileWriter.Flush();
            ReBootFileWriter.Close();

            string teststr = null;
            Regex testreg = new Regex("PROTOCOL: DHCP");//����������ʽ
            Match testmatch = testreg.Match(strRst2);//ƥ��õ������
            teststr = testmatch.Value.ToString();//��ȡƥ����



            if (teststr.Length == 0)
            {
                MessageBox.Show("��CM������/CMTS�϶���ѯ������CM��");

            }

            //ƥ��ip��ַ�Ĳ���
            else
            {
                string IP1 = null;

                string DHCP1 = null;

                Regex IPReg = new Regex("IP: " + @"\d{0,3}\.\d{0,3}\.\d{0,3}\.\d{0,3}");

                Regex DReg = new Regex("DATA: 192.168.222." + @"\d{0,3}");

                Match IPMatch = IPReg.Match(strRst2);

                Match DMatch = DReg.Match(strRst2);

                IP1 = IPMatch.Value.ToString();

                DHCP1 = DMatch.Value.ToString();

                string[] IPArray = Regex.Split(IP1, " ");

                string[] DArray = Regex.Split(DHCP1, " ");

                string IP = IPArray[1];

                string DHCP = DArray[1];

                //����CM

                string RebootPath = "C:\\Reboot.txt";
                File.Delete(RebootPath);
                string RebootCommand = "reboot docsis " + IPArray[1] + "\r\n" + "exit\r\n";

                FileStream RebootMacFile = new FileStream("C:\\Reboot.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter RebootMacFileWriter = new StreamWriter(RebootMacFile);
                RebootMacFileWriter.Flush();
                // ʹ��StreamWriter�����ļ���д������
                RebootMacFileWriter.BaseStream.Seek(0, SeekOrigin.Begin);
                // ��richTextBox1�е�����д���ļ�
                RebootMacFileWriter.Write(RebootCommand);
                RebootMacFileWriter.Flush();
                RebootMacFileWriter.Close();

                Process reb = new Process();
                reb.StartInfo.FileName = "cmd.exe";
                reb.StartInfo.UseShellExecute = false;
                reb.StartInfo.RedirectStandardInput = true;
                reb.StartInfo.RedirectStandardOutput = true;
                reb.StartInfo.RedirectStandardError = true;
                reb.StartInfo.CreateNoWindow = true;

                reb.Start();
                reb.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S " + DArray[1] + " -F C:\\Reboot.txt");
                reb.StandardInput.WriteLine("exit");

                string strRstreb = reb.StandardOutput.ReadToEnd();

                MessageBox.Show("�������ɹ���");
            }

            strInsert(mac, "����", Form2.strName);

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)//�����Ȩ��˼·���鵽һ�����һ��
        {
            string[] str = new string[11];
            //string mac = mac;

            //20170810
            string mac = comboBox5.Text;

            string command = "list search clientclassentry hardwareaddress " + mac + "\r\n" + "list search clientclassentry remoteID " + mac + "\r\n" + "exit\r\n";
            File.Delete("D:\\CM_MAC\\sfcce.txt");
            FileStream MacFile = new FileStream("D:\\CM_MAC\\sfcce.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter MacFileWriter = new StreamWriter(MacFile);
            MacFileWriter.Flush();
            // ʹ��StreamWriter�����ļ���д������
            MacFileWriter.BaseStream.Seek(0, SeekOrigin.Begin);
            // ������д���ļ�
            MacFileWriter.Write(command);
            MacFileWriter.Flush();
            MacFileWriter.Close();

            Process l = new Process();
            l.StartInfo.FileName = "cmd.exe";
            l.StartInfo.UseShellExecute = false;
            l.StartInfo.RedirectStandardInput = true;
            l.StartInfo.RedirectStandardOutput = true;
            l.StartInfo.RedirectStandardError = true;
            l.StartInfo.CreateNoWindow = true;

            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.101 -F D:\\CM_MAC\\sfcce.txt");
            l.StandardInput.WriteLine("exit");
            str[0] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.103 -F D:\\CM_MAC\\sfcce.txt");
            l.StandardInput.WriteLine("exit");
            str[1] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.105 -F D:\\CM_MAC\\sfcce.txt");
            l.StandardInput.WriteLine("exit");
            str[2] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.107 -F D:\\CM_MAC\\sfcce.txt");
            l.StandardInput.WriteLine("exit");
            str[3] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.109 -F D:\\CM_MAC\\sfcce.txt");
            l.StandardInput.WriteLine("exit");
            str[4] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.111 -F D:\\CM_MAC\\sfcce.txt");
            l.StandardInput.WriteLine("exit");
            str[5] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.113 -F D:\\CM_MAC\\sfcce.txt");
            l.StandardInput.WriteLine("exit");
            str[6] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.87 -F D:\\CM_MAC\\sfcce.txt");
            l.StandardInput.WriteLine("exit");
            str[7] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.91 -F D:\\CM_MAC\\sfcce.txt");
            l.StandardInput.WriteLine("exit");
            str[8] = l.StandardOutput.ReadToEnd();
            //20170704
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.93 -F D:\\CM_MAC\\sfcce.txt");
            l.StandardInput.WriteLine("exit");
            str[9] = l.StandardOutput.ReadToEnd();
            l.Start();
            l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.89 -F D:\\CM_MAC\\sfcce.txt");
            l.StandardInput.WriteLine("exit");
            str[10] = l.StandardOutput.ReadToEnd();
            
            string middle;
            string[] clientclass = new string[9];
            string command1 = null;//20170615��
            string[] cccc = new string[9];//20170615��
            string[] cccc1 = new string[9];//20170615��
            string[] cccc2 = new string[9];//20170615��
            string[] cccc3 = new string[9];//20170615��
            string[] cccc4 = new string[9];//20170615��
            string[] cccc5 = new string[9];//20170615��
            string[] cccc6 = new string[9];//20170615��
            string[] cccc7 = new string[9];//20170615��
            string[] cccc8 = new string[9];//20170615��

            string[] cccc9 = new string[9];//20170704��-------------
            string[] cccc10 = new string[9];//20170704��-------------


            int i = 0;//20170615��
            int a = 0;//��ʶ�Ƿ����Ϳ�

            Regex ccreg = new Regex(@"Client Classes: \w+");//������ʽ�ҳ� Client Classes:xxxxxx

            //-------------------------------------------------------------------------------------------0
            Match ccmatch = ccreg.Match(str[0]);//��ƥ������Ķ����Ž�ccmath
            
            File.Delete("D:\\CM_MAC\\sfcce0.txt");//20170615�� (ɾ����һ�ε�txt�ļ�)
            while (ccmatch.Success)
            {
                middle = ccmatch.Value.ToString();
                clientclass = Regex.Split(middle, ": ");//��ð��Ϊ�ָ���
               
                cccc[i] = clientclass[1];//�����ļ������ַ�������cccc������
                i += 1;
                
                ccmatch = ccmatch.NextMatch();

            }
           
            if (i > 0)//���ǿ�
            {
                a += 1;//��ʶ�ǿ�
                i -= 1;

                //������д��command1
                while (i >= 0)
                {
                    command1 += "modify clientclass " + cccc[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n";//+ "exit";
                    i -= 1;
                }
                command1 += "exit";

                // 20170614 ����д��txt�ļ�
                FileStream MacFile1 = new FileStream("D:\\CM_MAC\\sfcce0.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);//���ļ�����0λ�ÿ�ʼд
                MacFileWriter1.Write(command1);
                MacFileWriter1.Flush();
                MacFileWriter1.Close();

                l.Start();
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.101 -F D:\\CM_MAC\\sfcce0.txt");
                l.StandardInput.WriteLine("exit");
            }
            
            i = 0;//��ʼ�������õ�i
            command1 = null;//��ʼ��

            //----------------------------------------------------------------------------------1


            ccmatch = ccreg.Match(str[1]);//��ƥ������Ķ����Ž�ccmath

            File.Delete("D:\\CM_MAC\\sfcce1.txt");//20170615�� (ɾ����һ�ε�txt�ļ�)
            while (ccmatch.Success)
            {
                middle = ccmatch.Value.ToString();
                clientclass = Regex.Split(middle, ": ");//��ð��Ϊ�ָ���

                cccc1[i] = clientclass[1];//�����ļ������ַ�������cccc������
                i += 1;

                ccmatch = ccmatch.NextMatch();

            }

            if (i > 0)//���ǿ�
            {
                a += 1;//��ʶ�ǿ�
                i -= 1;

                //������д��command1
                while (i >= 0)
                {
                    command1 += "modify clientclass " + cccc1[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n";//+ "exit";
                    i -= 1;
                }
                command1 += "exit";

                // 20170614 ����д��txt�ļ�
                FileStream MacFile1 = new FileStream("D:\\CM_MAC\\sfcce1.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);//���ļ�����0λ�ÿ�ʼд
                MacFileWriter1.Write(command1);
                MacFileWriter1.Flush();
                MacFileWriter1.Close();

                l.Start();
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.103 -F D:\\CM_MAC\\sfcce1.txt");
                l.StandardInput.WriteLine("exit");
            }

            i = 0;//��ʼ�������õ�i
            command1 = null;//��ʼ��
            

            //----------------------------------------------------------------------------------2

             ccmatch = ccreg.Match(str[2]);//��ƥ������Ķ����Ž�ccmath

            File.Delete("D:\\CM_MAC\\sfcce2.txt");//20170615�� (ɾ����һ�ε�txt�ļ�)
            while (ccmatch.Success)
            {
                middle = ccmatch.Value.ToString();
                clientclass = Regex.Split(middle, ": ");//��ð��Ϊ�ָ���

                cccc2[i] = clientclass[1];//�����ļ������ַ�������cccc������
                i += 1;

                ccmatch = ccmatch.NextMatch();

            }

            if (i > 0)//���ǿ�
            {
                a += 1;//��ʶ�ǿ�
                i -= 1;

                //������д��command1
                while (i >= 0)
                {
                    command1 += "modify clientclass " + cccc2[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n";//+ "exit";
                    i -= 1;
                }
                command1 += "exit";

                // 20170614 ����д��txt�ļ�
                FileStream MacFile1 = new FileStream("D:\\CM_MAC\\sfcce2.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);//���ļ�����0λ�ÿ�ʼд
                MacFileWriter1.Write(command1);
                MacFileWriter1.Flush();
                MacFileWriter1.Close();

                l.Start();
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.105 -F D:\\CM_MAC\\sfcce2.txt");
                l.StandardInput.WriteLine("exit");

            }
            i = 0;//��ʼ�������õ�i
            command1 = null;//��ʼ��
           

            //----------------------------------------------------------------------------------3

            ccmatch = ccreg.Match(str[3]);//��ƥ������Ķ����Ž�ccmath

            File.Delete("D:\\CM_MAC\\sfcce3.txt");//20170615�� (ɾ����һ�ε�txt�ļ�)
            while (ccmatch.Success)
            {
                middle = ccmatch.Value.ToString();
                clientclass = Regex.Split(middle, ": ");//��ð��Ϊ�ָ���

                cccc3[i] = clientclass[1];//�����ļ������ַ�������cccc������
                i += 1;

                ccmatch = ccmatch.NextMatch();

            }
            if (i > 0)//���ǿ�
            {
                a += 1;//��ʶ�ǿ�
                i -= 1;

                //������д��command1
                while (i >= 0)
                {
                    command1 += "modify clientclass " + cccc3[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n";//+ "exit";
                    i -= 1;
                }
                command1 += "exit";

                // 20170614 ����д��txt�ļ�
                FileStream MacFile1 = new FileStream("D:\\CM_MAC\\sfcce3.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);//���ļ�����0λ�ÿ�ʼд
                MacFileWriter1.Write(command1);
                MacFileWriter1.Flush();
                MacFileWriter1.Close();

                l.Start();
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.107 -F D:\\CM_MAC\\sfcce3.txt");
                l.StandardInput.WriteLine("exit");
            }
            i = 0;//��ʼ�������õ�i
            command1 = null;//��ʼ��
            

            //----------------------------------------------------------------------------------4

            ccmatch = ccreg.Match(str[4]);//��ƥ������Ķ����Ž�ccmath

            File.Delete("D:\\CM_MAC\\sfcce4.txt");//20170615�� (ɾ����һ�ε�txt�ļ�)
            while (ccmatch.Success)
            {
                middle = ccmatch.Value.ToString();
                clientclass = Regex.Split(middle, ": ");//��ð��Ϊ�ָ���

                cccc4[i] = clientclass[1];//�����ļ������ַ�������cccc������
                i += 1;

                ccmatch = ccmatch.NextMatch();

            }
            if (i > 0)//���ǿ�
            {
                a += 1;//��ʶ�ǿ�
                i -= 1;

                //������д��command1
                while (i >= 0)
                {
                    command1 += "modify clientclass " + cccc4[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n";//+ "exit";
                    i -= 1;
                }
                command1 += "exit";

                // 20170614 ����д��txt�ļ�
                FileStream MacFile1 = new FileStream("D:\\CM_MAC\\sfcce4.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);//���ļ�����0λ�ÿ�ʼд
                MacFileWriter1.Write(command1);
                MacFileWriter1.Flush();
                MacFileWriter1.Close();

                l.Start();
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.109 -F D:\\CM_MAC\\sfcce4.txt");
                l.StandardInput.WriteLine("exit");
            }
            i = 0;//��ʼ�������õ�i
            command1 = null;//��ʼ��
          

            //----------------------------------------------------------------------------------5

            ccmatch = ccreg.Match(str[5]);//��ƥ������Ķ����Ž�ccmath

            File.Delete("D:\\CM_MAC\\sfcce5.txt");//20170615�� (ɾ����һ�ε�txt�ļ�)
            while (ccmatch.Success)
            {
                middle = ccmatch.Value.ToString();
                clientclass = Regex.Split(middle, ": ");//��ð��Ϊ�ָ���

                cccc5[i] = clientclass[1];//�����ļ������ַ�������cccc������
                i += 1;

                ccmatch = ccmatch.NextMatch();

            }
            if (i > 0)//���ǿ�
            {
                a += 1;//��ʶ�ǿ�
                i -= 1;

                //������д��command1
                while (i >= 0)
                {
                    command1 += "modify clientclass " + cccc5[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n";//+ "exit";
                    i -= 1;
                }
                command1 += "exit";

                // 20170614 ����д��txt�ļ�
                FileStream MacFile1 = new FileStream("D:\\CM_MAC\\sfcce5.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);//���ļ�����0λ�ÿ�ʼд
                MacFileWriter1.Write(command1);
                MacFileWriter1.Flush();
                MacFileWriter1.Close();

                l.Start();
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.111 -F D:\\CM_MAC\\sfcce5.txt");
                l.StandardInput.WriteLine("exit");
            }
            i = 0;//��ʼ�������õ�i
            command1 = null;//��ʼ��
          

            //----------------------------------------------------------------------------------6

            ccmatch = ccreg.Match(str[6]);//��ƥ������Ķ����Ž�ccmath

            File.Delete("D:\\CM_MAC\\sfcce6.txt");//20170615�� (ɾ����һ�ε�txt�ļ�)
            while (ccmatch.Success)
            {
                middle = ccmatch.Value.ToString();
                clientclass = Regex.Split(middle, ": ");//��ð��Ϊ�ָ���

                cccc6[i] = clientclass[1];//�����ļ������ַ�������cccc������
                i += 1;

                ccmatch = ccmatch.NextMatch();

            }
            if (i > 0)//���ǿ�
            {
                a += 1;//��ʶ�ǿ�
                i -= 1;

                //������д��command1
                while (i >= 0)
                {
                    command1 += "modify clientclass " + cccc6[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n";//+ "exit";
                    i -= 1;
                }
                command1 += "exit";

                // 20170614 ����д��txt�ļ�
                FileStream MacFile1 = new FileStream("D:\\CM_MAC\\sfcce6.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);//���ļ�����0λ�ÿ�ʼд
                MacFileWriter1.Write(command1);
                MacFileWriter1.Flush();
                MacFileWriter1.Close();

                l.Start();
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.113 -F D:\\CM_MAC\\sfcce6.txt");
                l.StandardInput.WriteLine("exit");
            }
            i = 0;//��ʼ�������õ�i
            command1 = null;//��ʼ��
         

            //----------------------------------------------------------------------------------7

            ccmatch = ccreg.Match(str[7]);//��ƥ������Ķ����Ž�ccmath

            File.Delete("D:\\CM_MAC\\sfcce7.txt");//20170615�� (ɾ����һ�ε�txt�ļ�)
            while (ccmatch.Success)
            {
                middle = ccmatch.Value.ToString();
                clientclass = Regex.Split(middle, ": ");//��ð��Ϊ�ָ���

                cccc7[i] = clientclass[1];//�����ļ������ַ�������cccc������
                i += 1;

                ccmatch = ccmatch.NextMatch();

            }
            if (i > 0)//���ǿ�
            {
                a += 1;//��ʶ�ǿ�
                i -= 1;

                //������д��command1
                while (i >= 0)
                {
                    command1 += "modify clientclass " + cccc7[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n";//+ "exit";
                    i -= 1;
                }
                command1 += "exit";

                // 20170614 ����д��txt�ļ�
                FileStream MacFile1 = new FileStream("D:\\CM_MAC\\sfcce7.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);//���ļ�����0λ�ÿ�ʼд
                MacFileWriter1.Write(command1);
                MacFileWriter1.Flush();
                MacFileWriter1.Close();

                l.Start();
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.87 -F D:\\CM_MAC\\sfcce7.txt");
                l.StandardInput.WriteLine("exit");
            }
            i = 0;//��ʼ�������õ�i
            command1 = null;//��ʼ��
         

            //----------------------------------------------------------------------------------8

            ccmatch = ccreg.Match(str[8]);//��ƥ������Ķ����Ž�ccmath

            File.Delete("D:\\CM_MAC\\sfcce8.txt");//20170615�� (ɾ����һ�ε�txt�ļ�)
            while (ccmatch.Success)
            {
                middle = ccmatch.Value.ToString();
                clientclass = Regex.Split(middle, ": ");//��ð��Ϊ�ָ���

                cccc8[i] = clientclass[1];//�����ļ������ַ�������cccc������
                i += 1;

                ccmatch = ccmatch.NextMatch();

            }
            if (i > 0)//���ǿ�
            {
                a += 1;//��ʶ�ǿ�
                i -= 1;

                //������д��command1
                while (i >= 0)
                {
                    command1 += "modify clientclass " + cccc8[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n";//+ "exit";
                    i -= 1;
                }
                command1 += "exit";

                // 20170614 ����д��txt�ļ�
                FileStream MacFile1 = new FileStream("D:\\CM_MAC\\sfcce8.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);//���ļ�����0λ�ÿ�ʼд
                MacFileWriter1.Write(command1);
                MacFileWriter1.Flush();
                MacFileWriter1.Close();

                l.Start();
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.91 -F D:\\CM_MAC\\sfcce8.txt");
                l.StandardInput.WriteLine("exit");
            }
            i = 0;//��ʼ�������õ�i
            command1 = null;//��ʼ��


            //----------20170704----------------------
            //-------------------------------------------------------------------------------------------9
            ccmatch = ccreg.Match(str[9]);//��ƥ������Ķ����Ž�ccmath

            File.Delete("D:\\CM_MAC\\sfcce9.txt");//20170615�� (ɾ����һ�ε�txt�ļ�)
            while (ccmatch.Success)
            {
                middle = ccmatch.Value.ToString();
                clientclass = Regex.Split(middle, ": ");//��ð��Ϊ�ָ���

                cccc[i] = clientclass[1];//�����ļ������ַ�������cccc������
                i += 1;

                ccmatch = ccmatch.NextMatch();

            }

            if (i > 0)//���ǿ�
            {
                a += 1;//��ʶ�ǿ�
                i -= 1;

                //������д��command1
                while (i >= 0)
                {
                    command1 += "modify clientclass " + cccc[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n";//+ "exit";
                    i -= 1;
                }
                command1 += "exit";

                // 20170614 ����д��txt�ļ�
                FileStream MacFile1 = new FileStream("D:\\CM_MAC\\sfcce0.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);//���ļ�����0λ�ÿ�ʼд
                MacFileWriter1.Write(command1);
                MacFileWriter1.Flush();
                MacFileWriter1.Close();

                l.Start();
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.93 -F D:\\CM_MAC\\sfcce0.txt");
                l.StandardInput.WriteLine("exit");
            }

            i = 0;//��ʼ�������õ�i
            command1 = null;//��ʼ��

            //-------------------------------------------------------------------------------------------10
            ccmatch = ccreg.Match(str[10]);//��ƥ������Ķ����Ž�ccmath

            File.Delete("D:\\CM_MAC\\sfcce10.txt");//20170615�� (ɾ����һ�ε�txt�ļ�)
            while (ccmatch.Success)
            {
                middle = ccmatch.Value.ToString();
                clientclass = Regex.Split(middle, ": ");//��ð��Ϊ�ָ���

                cccc[i] = clientclass[1];//�����ļ������ַ�������cccc������
                i += 1;

                ccmatch = ccmatch.NextMatch();

            }

            if (i > 0)//���ǿ�
            {
                a += 1;//��ʶ�ǿ�
                i -= 1;

                //������д��command1
                while (i >= 0)
                {
                    command1 += "modify clientclass " + cccc[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n";//+ "exit";
                    i -= 1;
                }
                command1 += "exit";

                // 20170614 ����д��txt�ļ�
                FileStream MacFile1 = new FileStream("D:\\CM_MAC\\sfcce0.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);//���ļ�����0λ�ÿ�ʼд
                MacFileWriter1.Write(command1);
                MacFileWriter1.Flush();
                MacFileWriter1.Close();

                l.Start();
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.89 -F D:\\CM_MAC\\sfcce0.txt");
                l.StandardInput.WriteLine("exit");
            }

            i = 0;//��ʼ�������õ�i
            command1 = null;//��ʼ��

        

            //----------------------------------------------------------------------------------
            if (a != 0)
            {
                strInsert(mac, "�����Ȩ", Form2.strName);
                MessageBox.Show("�����Ȩ��ϣ�");
            }
            else {
                MessageBox.Show("�������ǿյ���Ȩ��");
            }
            

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)//���������Ȩ20170616
        {
            //20170616�ӡ����������Ȩ����,˼·���ȶ�ȡtxt��ÿ��mac��ַ����������cm�����Ȩ��ѭ��
            string[] mac = new string[200000];//�����������װÿ��mac
            FileStream MacListFileR = new FileStream(MacList, FileMode.Open, FileAccess.Read);
            StreamReader MacListFileReader = new StreamReader(MacListFileR);
            MacListFileReader.BaseStream.Seek(0, SeekOrigin.Begin);
            string strLine = MacListFileReader.ReadLine();
            int ins = 0;
            while (strLine != null) {
                mac[ins] = strLine;
                ins++;
                strLine = MacListFileReader.ReadLine();
            }

            for (int aa = 0; aa < ins; aa = aa + 1)
            {

                string[] str = new string[11];//��һ���Ȳ�ѯcc
                
                string command = "list search clientclassentry hardwareaddress " + mac[aa] + "\r\n" + "list search clientclassentry remoteID " + mac[aa] + "\r\n" + "exit\r\n";
                File.Delete("D:\\CM_MAC\\sfcce.txt");
                FileStream MacFile = new FileStream("D:\\CM_MAC\\sfcce.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter MacFileWriter = new StreamWriter(MacFile);
                MacFileWriter.Flush();
                // ʹ��StreamWriter�����ļ���д������
                MacFileWriter.BaseStream.Seek(0, SeekOrigin.Begin);
                // ��richtextBox11�е�����д���ļ�
                MacFileWriter.Write(command);
                MacFileWriter.Flush();
                MacFileWriter.Close();

                Process l = new Process();
                l.StartInfo.FileName = "cmd.exe";
                l.StartInfo.UseShellExecute = false;
                l.StartInfo.RedirectStandardInput = true;
                l.StartInfo.RedirectStandardOutput = true;
                l.StartInfo.RedirectStandardError = true;
                l.StartInfo.CreateNoWindow = true;

                l.Start();
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.101 -F D:\\CM_MAC\\sfcce.txt");
                l.StandardInput.WriteLine("exit");
                str[0] = l.StandardOutput.ReadToEnd();
                l.Start();
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.103 -F D:\\CM_MAC\\sfcce.txt");
                l.StandardInput.WriteLine("exit");
                str[1] = l.StandardOutput.ReadToEnd();
                l.Start();
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.105 -F D:\\CM_MAC\\sfcce.txt");
                l.StandardInput.WriteLine("exit");
                str[2] = l.StandardOutput.ReadToEnd();
                l.Start();
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.107 -F D:\\CM_MAC\\sfcce.txt");
                l.StandardInput.WriteLine("exit");
                str[3] = l.StandardOutput.ReadToEnd();
                l.Start();
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.109 -F D:\\CM_MAC\\sfcce.txt");
                l.StandardInput.WriteLine("exit");
                str[4] = l.StandardOutput.ReadToEnd();
                l.Start();
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.111 -F D:\\CM_MAC\\sfcce.txt");
                l.StandardInput.WriteLine("exit");
                str[5] = l.StandardOutput.ReadToEnd();
                l.Start();
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.113 -F D:\\CM_MAC\\sfcce.txt");
                l.StandardInput.WriteLine("exit");
                str[6] = l.StandardOutput.ReadToEnd();
                l.Start();
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.87 -F D:\\CM_MAC\\sfcce.txt");
                l.StandardInput.WriteLine("exit");
                str[7] = l.StandardOutput.ReadToEnd();
                l.Start();
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.91 -F D:\\CM_MAC\\sfcce.txt");
                l.StandardInput.WriteLine("exit");
                str[8] = l.StandardOutput.ReadToEnd();
                //20170704
                l.Start();
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.93 -F D:\\CM_MAC\\sfcce.txt");
                l.StandardInput.WriteLine("exit");
                str[9] = l.StandardOutput.ReadToEnd();
                l.Start();
                l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.89 -F D:\\CM_MAC\\sfcce.txt");
                l.StandardInput.WriteLine("exit");
                str[10] = l.StandardOutput.ReadToEnd();


                string middle;
                string[] clientclass = new string[9];
                string command1 = null;//20170615��
                string[] cccc = new string[9];//20170615��
                string[] cccc1 = new string[9];//20170615��
                string[] cccc2 = new string[9];//20170615��
                string[] cccc3 = new string[9];//20170615��
                string[] cccc4 = new string[9];//20170615��
                string[] cccc5 = new string[9];//20170615��
                string[] cccc6 = new string[9];//20170615��
                string[] cccc7 = new string[9];//20170615��
                string[] cccc8 = new string[9];//20170615��

                string[] cccc9 = new string[9];//20170704��
                string[] cccc10 = new string[9];//20170704��


                int i = 0;//20170615��
                int a = 0;//��ʶ�Ƿ����Ϳ�


                Regex ccreg = new Regex(@"Client Classes: \w+");//������ʽ�ҳ� Client Classes:xxxxxx

                //-------------------------------------------------------------------------------------------0
                Match ccmatch = ccreg.Match(str[0]);//��ƥ������Ķ����Ž�ccmath

                File.Delete("D:\\CM_MAC\\sfcce0.txt");//20170615�� (ɾ����һ�ε�txt�ļ�)
                while (ccmatch.Success)
                {
                    middle = ccmatch.Value.ToString();
                    clientclass = Regex.Split(middle, ": ");//��ð��Ϊ�ָ���

                    cccc[i] = clientclass[1];//�����ļ������ַ�������cccc������
                    i += 1;

                    ccmatch = ccmatch.NextMatch();

                }

                if (i > 0)//���ǿ�
                {
                    a += 1;//��ʶ�ǿ�
                    i -= 1;

                    //������д��command1
                    while (i >= 0)
                    {
                        command1 += "modify clientclass " + cccc[i] + "\r\n" + "delete clientclassentry " + mac[aa] + "\r\n" + "save\r\n" + "exit\r\n";//+ "exit";
                        i -= 1;
                    }
                    command1 += "exit";

                    // 20170614 ����д��txt�ļ�
                    FileStream MacFile1 = new FileStream("D:\\CM_MAC\\sfcce0.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                    MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);//���ļ�����0λ�ÿ�ʼд
                    MacFileWriter1.Write(command1);
                    MacFileWriter1.Flush();
                    MacFileWriter1.Close();

                    l.Start();
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.101 -F D:\\CM_MAC\\sfcce0.txt");
                    l.StandardInput.WriteLine("exit");
                }

                i = 0;//��ʼ�������õ�i
                command1 = null;//��ʼ��

                //----------------------------------------------------------------------------------1


                ccmatch = ccreg.Match(str[1]);//��ƥ������Ķ����Ž�ccmath

                File.Delete("D:\\CM_MAC\\sfcce1.txt");//20170615�� (ɾ����һ�ε�txt�ļ�)
                while (ccmatch.Success)
                {
                    middle = ccmatch.Value.ToString();
                    clientclass = Regex.Split(middle, ": ");//��ð��Ϊ�ָ���

                    cccc1[i] = clientclass[1];//�����ļ������ַ�������cccc������
                    i += 1;

                    ccmatch = ccmatch.NextMatch();

                }

                if (i > 0)//���ǿ�
                {
                    a += 1;//��ʶ�ǿ�
                    i -= 1;

                    //������д��command1
                    while (i >= 0)
                    {
                        command1 += "modify clientclass " + cccc1[i] + "\r\n" + "delete clientclassentry " + mac[aa] + "\r\n" + "save\r\n" + "exit\r\n";//+ "exit";
                        i -= 1;
                    }
                    command1 += "exit";

                    // 20170614 ����д��txt�ļ�
                    FileStream MacFile1 = new FileStream("D:\\CM_MAC\\sfcce1.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                    MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);//���ļ�����0λ�ÿ�ʼд
                    MacFileWriter1.Write(command1);
                    MacFileWriter1.Flush();
                    MacFileWriter1.Close();

                    l.Start();
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.103 -F D:\\CM_MAC\\sfcce1.txt");
                    l.StandardInput.WriteLine("exit");
                }

                i = 0;//��ʼ�������õ�i
                command1 = null;//��ʼ��


                //----------------------------------------------------------------------------------2

                ccmatch = ccreg.Match(str[2]);//��ƥ������Ķ����Ž�ccmath

                File.Delete("D:\\CM_MAC\\sfcce2.txt");//20170615�� (ɾ����һ�ε�txt�ļ�)
                while (ccmatch.Success)
                {
                    middle = ccmatch.Value.ToString();
                    clientclass = Regex.Split(middle, ": ");//��ð��Ϊ�ָ���

                    cccc2[i] = clientclass[1];//�����ļ������ַ�������cccc������
                    i += 1;

                    ccmatch = ccmatch.NextMatch();

                }

                if (i > 0)//���ǿ�
                {
                    a += 1;//��ʶ�ǿ�
                    i -= 1;

                    //������д��command1
                    while (i >= 0)
                    {
                        command1 += "modify clientclass " + cccc2[i] + "\r\n" + "delete clientclassentry " + mac[aa] + "\r\n" + "save\r\n" + "exit\r\n";//+ "exit";
                        i -= 1;
                    }
                    command1 += "exit";

                    // 20170614 ����д��txt�ļ�
                    FileStream MacFile1 = new FileStream("D:\\CM_MAC\\sfcce2.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                    MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);//���ļ�����0λ�ÿ�ʼд
                    MacFileWriter1.Write(command1);
                    MacFileWriter1.Flush();
                    MacFileWriter1.Close();

                    l.Start();
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.105 -F D:\\CM_MAC\\sfcce2.txt");
                    l.StandardInput.WriteLine("exit");

                }
                i = 0;//��ʼ�������õ�i
                command1 = null;//��ʼ��


                //----------------------------------------------------------------------------------3

                ccmatch = ccreg.Match(str[3]);//��ƥ������Ķ����Ž�ccmath

                File.Delete("D:\\CM_MAC\\sfcce3.txt");//20170615�� (ɾ����һ�ε�txt�ļ�)
                while (ccmatch.Success)
                {
                    middle = ccmatch.Value.ToString();
                    clientclass = Regex.Split(middle, ": ");//��ð��Ϊ�ָ���

                    cccc3[i] = clientclass[1];//�����ļ������ַ�������cccc������
                    i += 1;

                    ccmatch = ccmatch.NextMatch();

                }
                if (i > 0)//���ǿ�
                {
                    a += 1;//��ʶ�ǿ�
                    i -= 1;

                    //������д��command1
                    while (i >= 0)
                    {
                        command1 += "modify clientclass " + cccc3[i] + "\r\n" + "delete clientclassentry " + mac[aa] + "\r\n" + "save\r\n" + "exit\r\n";//+ "exit";
                        i -= 1;
                    }
                    command1 += "exit";

                    // 20170614 ����д��txt�ļ�
                    FileStream MacFile1 = new FileStream("D:\\CM_MAC\\sfcce3.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                    MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);//���ļ�����0λ�ÿ�ʼд
                    MacFileWriter1.Write(command1);
                    MacFileWriter1.Flush();
                    MacFileWriter1.Close();

                    l.Start();
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.107 -F D:\\CM_MAC\\sfcce3.txt");
                    l.StandardInput.WriteLine("exit");
                }
                i = 0;//��ʼ�������õ�i
                command1 = null;//��ʼ��


                //----------------------------------------------------------------------------------4

                ccmatch = ccreg.Match(str[4]);//��ƥ������Ķ����Ž�ccmath

                File.Delete("D:\\CM_MAC\\sfcce4.txt");//20170615�� (ɾ����һ�ε�txt�ļ�)
                while (ccmatch.Success)
                {
                    middle = ccmatch.Value.ToString();
                    clientclass = Regex.Split(middle, ": ");//��ð��Ϊ�ָ���

                    cccc4[i] = clientclass[1];//�����ļ������ַ�������cccc������
                    i += 1;

                    ccmatch = ccmatch.NextMatch();

                }
                if (i > 0)//���ǿ�
                {
                    a += 1;//��ʶ�ǿ�
                    i -= 1;

                    //������д��command1
                    while (i >= 0)
                    {
                        command1 += "modify clientclass " + cccc4[i] + "\r\n" + "delete clientclassentry " + mac[aa] + "\r\n" + "save\r\n" + "exit\r\n";//+ "exit";
                        i -= 1;
                    }
                    command1 += "exit";

                    // 20170614 ����д��txt�ļ�
                    FileStream MacFile1 = new FileStream("D:\\CM_MAC\\sfcce4.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                    MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);//���ļ�����0λ�ÿ�ʼд
                    MacFileWriter1.Write(command1);
                    MacFileWriter1.Flush();
                    MacFileWriter1.Close();

                    l.Start();
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.109 -F D:\\CM_MAC\\sfcce4.txt");
                    l.StandardInput.WriteLine("exit");
                }
                i = 0;//��ʼ�������õ�i
                command1 = null;//��ʼ��


                //----------------------------------------------------------------------------------5

                ccmatch = ccreg.Match(str[5]);//��ƥ������Ķ����Ž�ccmath

                File.Delete("D:\\CM_MAC\\sfcce5.txt");//20170615�� (ɾ����һ�ε�txt�ļ�)
                while (ccmatch.Success)
                {
                    middle = ccmatch.Value.ToString();
                    clientclass = Regex.Split(middle, ": ");//��ð��Ϊ�ָ���

                    cccc5[i] = clientclass[1];//�����ļ������ַ�������cccc������
                    i += 1;

                    ccmatch = ccmatch.NextMatch();

                }
                if (i > 0)//���ǿ�
                {
                    a += 1;//��ʶ�ǿ�
                    i -= 1;

                    //������д��command1
                    while (i >= 0)
                    {
                        command1 += "modify clientclass " + cccc5[i] + "\r\n" + "delete clientclassentry " + mac[aa] + "\r\n" + "save\r\n" + "exit\r\n";//+ "exit";
                        i -= 1;
                    }
                    command1 += "exit";

                    // 20170614 ����д��txt�ļ�
                    FileStream MacFile1 = new FileStream("D:\\CM_MAC\\sfcce5.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                    MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);//���ļ�����0λ�ÿ�ʼд
                    MacFileWriter1.Write(command1);
                    MacFileWriter1.Flush();
                    MacFileWriter1.Close();

                    l.Start();
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.111 -F D:\\CM_MAC\\sfcce5.txt");
                    l.StandardInput.WriteLine("exit");
                }
                i = 0;//��ʼ�������õ�i
                command1 = null;//��ʼ��


                //----------------------------------------------------------------------------------6

                ccmatch = ccreg.Match(str[6]);//��ƥ������Ķ����Ž�ccmath

                File.Delete("D:\\CM_MAC\\sfcce6.txt");//20170615�� (ɾ����һ�ε�txt�ļ�)
                while (ccmatch.Success)
                {
                    middle = ccmatch.Value.ToString();
                    clientclass = Regex.Split(middle, ": ");//��ð��Ϊ�ָ���

                    cccc6[i] = clientclass[1];//�����ļ������ַ�������cccc������
                    i += 1;

                    ccmatch = ccmatch.NextMatch();

                }
                if (i > 0)//���ǿ�
                {
                    a += 1;//��ʶ�ǿ�
                    i -= 1;

                    //������д��command1
                    while (i >= 0)
                    {
                        command1 += "modify clientclass " + cccc6[i] + "\r\n" + "delete clientclassentry " + mac[aa] + "\r\n" + "save\r\n" + "exit\r\n";//+ "exit";
                        i -= 1;
                    }
                    command1 += "exit";

                    // 20170614 ����д��txt�ļ�
                    FileStream MacFile1 = new FileStream("D:\\CM_MAC\\sfcce6.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                    MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);//���ļ�����0λ�ÿ�ʼд
                    MacFileWriter1.Write(command1);
                    MacFileWriter1.Flush();
                    MacFileWriter1.Close();

                    l.Start();
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.113 -F D:\\CM_MAC\\sfcce6.txt");
                    l.StandardInput.WriteLine("exit");
                }
                i = 0;//��ʼ�������õ�i
                command1 = null;//��ʼ��


                //----------------------------------------------------------------------------------7

                ccmatch = ccreg.Match(str[7]);//��ƥ������Ķ����Ž�ccmath

                File.Delete("D:\\CM_MAC\\sfcce7.txt");//20170615�� (ɾ����һ�ε�txt�ļ�)
                while (ccmatch.Success)
                {
                    middle = ccmatch.Value.ToString();
                    clientclass = Regex.Split(middle, ": ");//��ð��Ϊ�ָ���

                    cccc7[i] = clientclass[1];//�����ļ������ַ�������cccc������
                    i += 1;

                    ccmatch = ccmatch.NextMatch();

                }
                if (i > 0)//���ǿ�
                {
                    a += 1;//��ʶ�ǿ�
                    i -= 1;

                    //������д��command1
                    while (i >= 0)
                    {
                        command1 += "modify clientclass " + cccc7[i] + "\r\n" + "delete clientclassentry " + mac[aa] + "\r\n" + "save\r\n" + "exit\r\n";//+ "exit";
                        i -= 1;
                    }
                    command1 += "exit";

                    // 20170614 ����д��txt�ļ�
                    FileStream MacFile1 = new FileStream("D:\\CM_MAC\\sfcce7.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                    MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);//���ļ�����0λ�ÿ�ʼд
                    MacFileWriter1.Write(command1);
                    MacFileWriter1.Flush();
                    MacFileWriter1.Close();

                    l.Start();
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.87 -F D:\\CM_MAC\\sfcce7.txt");
                    l.StandardInput.WriteLine("exit");
                }
                i = 0;//��ʼ�������õ�i
                command1 = null;//��ʼ��


                //----------------------------------------------------------------------------------8

                ccmatch = ccreg.Match(str[8]);//��ƥ������Ķ����Ž�ccmath

                File.Delete("D:\\CM_MAC\\sfcce8.txt");//20170615�� (ɾ����һ�ε�txt�ļ�)
                while (ccmatch.Success)
                {
                    middle = ccmatch.Value.ToString();
                    clientclass = Regex.Split(middle, ": ");//��ð��Ϊ�ָ���

                    cccc8[i] = clientclass[1];//�����ļ������ַ�������cccc������
                    i += 1;

                    ccmatch = ccmatch.NextMatch();

                }
                if (i > 0)//���ǿ�
                {
                    a += 1;//��ʶ�ǿ�
                    i -= 1;

                    //������д��command1
                    while (i >= 0)
                    {
                        command1 += "modify clientclass " + cccc8[i] + "\r\n" + "delete clientclassentry " + mac[aa] + "\r\n" + "save\r\n" + "exit\r\n";//+ "exit";
                        i -= 1;
                    }
                    command1 += "exit";

                    // 20170614 ����д��txt�ļ�
                    FileStream MacFile1 = new FileStream("D:\\CM_MAC\\sfcce8.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                    MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);//���ļ�����0λ�ÿ�ʼд
                    MacFileWriter1.Write(command1);
                    MacFileWriter1.Flush();
                    MacFileWriter1.Close();

                    l.Start();
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.91 -F D:\\CM_MAC\\sfcce8.txt");
                    l.StandardInput.WriteLine("exit");
                }
                i = 0;//��ʼ�������õ�i
                command1 = null;//��ʼ��

                //----------20170704----------------------
                //-------------------------------------------------------------------------------------------9
                ccmatch = ccreg.Match(str[9]);//��ƥ������Ķ����Ž�ccmath

                File.Delete("D:\\CM_MAC\\sfcce9.txt");//20170615�� (ɾ����һ�ε�txt�ļ�)
                while (ccmatch.Success)
                {
                    middle = ccmatch.Value.ToString();
                    clientclass = Regex.Split(middle, ": ");//��ð��Ϊ�ָ���

                    cccc[i] = clientclass[1];//�����ļ������ַ�������cccc������
                    i += 1;

                    ccmatch = ccmatch.NextMatch();

                }

                if (i > 0)//���ǿ�
                {
                    a += 1;//��ʶ�ǿ�
                    i -= 1;

                    //������д��command1
                    while (i >= 0)
                    {
                        command1 += "modify clientclass " + cccc[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n";//+ "exit";
                        i -= 1;
                    }
                    command1 += "exit";

                    // 20170614 ����д��txt�ļ�
                    FileStream MacFile1 = new FileStream("D:\\CM_MAC\\sfcce0.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                    MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);//���ļ�����0λ�ÿ�ʼд
                    MacFileWriter1.Write(command1);
                    MacFileWriter1.Flush();
                    MacFileWriter1.Close();

                    l.Start();
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.93 -F D:\\CM_MAC\\sfcce0.txt");
                    l.StandardInput.WriteLine("exit");
                }

                i = 0;//��ʼ�������õ�i
                command1 = null;//��ʼ��

                //-------------------------------------------------------------------------------------------10
                ccmatch = ccreg.Match(str[10]);//��ƥ������Ķ����Ž�ccmath

                File.Delete("D:\\CM_MAC\\sfcce10.txt");//20170615�� (ɾ����һ�ε�txt�ļ�)
                while (ccmatch.Success)
                {
                    middle = ccmatch.Value.ToString();
                    clientclass = Regex.Split(middle, ": ");//��ð��Ϊ�ָ���

                    cccc[i] = clientclass[1];//�����ļ������ַ�������cccc������
                    i += 1;

                    ccmatch = ccmatch.NextMatch();

                }

                if (i > 0)//���ǿ�
                {
                    a += 1;//��ʶ�ǿ�
                    i -= 1;

                    //������д��command1
                    while (i >= 0)
                    {
                        command1 += "modify clientclass " + cccc[i] + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n";//+ "exit";
                        i -= 1;
                    }
                    command1 += "exit";

                    // 20170614 ����д��txt�ļ�
                    FileStream MacFile1 = new FileStream("D:\\CM_MAC\\sfcce0.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter MacFileWriter1 = new StreamWriter(MacFile1);
                    MacFileWriter1.BaseStream.Seek(0, SeekOrigin.Begin);//���ļ�����0λ�ÿ�ʼд
                    MacFileWriter1.Write(command1);
                    MacFileWriter1.Flush();
                    MacFileWriter1.Close();

                    l.Start();
                    l.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222.89 -F D:\\CM_MAC\\sfcce0.txt");
                    l.StandardInput.WriteLine("exit");
                }

                i = 0;//��ʼ�������õ�i
                command1 = null;//��ʼ��


                //----------------------------------------------------------------------------------
               /* if (a != 0)
                {
                    strInsert(mac, "�����Ȩ", Form2.strName);
                    MessageBox.Show("�����Ȩ��ϣ�");
                }
                else
                {
                    MessageBox.Show("�������ǿյ���Ȩ��");
                }*/
                strInsert(mac[aa], "���������Ȩ", Form2.strName);//���ݿ��¼
            }
            
            MessageBox.Show("���������Ȩ��ϣ�");
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            string CCPath = "C:\\CCListfile.txt";
            File.Delete(CCPath);
            string Command = "";
            
            string cccdhcp = comboBox6.SelectedItem.ToString();

            //20170810
            string mac = comboBox5.Text;

            string ccInfo = comboBox7.SelectedItem.ToString();
          
            Command = "modify clientclass " + ccInfo + "\r\n" + "add clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n";


            FileStream CCListFileW = new FileStream("C:\\CCListfile.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter CCListFileWriter = new StreamWriter(CCListFileW);
            CCListFileWriter.Flush();
            // ʹ��StreamWriter�����ļ���д������
            CCListFileWriter.BaseStream.Seek(0, SeekOrigin.Begin);
            // ��richTextBox1�е�����д���ļ�
            //Command += "exit\r\n";
            CCListFileWriter.Write(Command);
            //�رմ��ļ�;
            CCListFileWriter.Flush();
            CCListFileWriter.Close();

            Process CC = new Process();
            CC.StartInfo.FileName = "cmd.exe";
            CC.StartInfo.UseShellExecute = false;
            CC.StartInfo.RedirectStandardInput = true;
            CC.StartInfo.RedirectStandardOutput = true;
            CC.StartInfo.RedirectStandardError = true;
            CC.StartInfo.CreateNoWindow = true;

            CC.Start();
            CC.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222." + cccdhcp + " -F C:\\CCListfile.txt");
            CC.StandardInput.WriteLine("exit");

            string strRst = CC.StandardOutput.ReadToEnd();

            //strInsertss(,Mac, "���DHCP" + cccdhcp + "��Ȩ" + ccInfo, Form2.strName);
            strInsert(mac, "���DHCP" + cccdhcp + "��Ȩ" + ccInfo, Form2.strName);


            MessageBox.Show("�����ɹ���");

        }

        private void button16_Click(object sender, EventArgs e)
        {
            string CCPath = "C:\\CCListfile.txt";
            File.Delete(CCPath);
            string Command = "";

            string cccdhcp = comboBox6.SelectedItem.ToString();

            //20170810
            string mac = comboBox5.Text;


            string ccInfo = comboBox7.SelectedItem.ToString();

            Command = "modify clientclass " + ccInfo + "\r\n" + "delete clientclassentry " + mac + "\r\n" + "save\r\n" + "exit\r\n";


            FileStream CCListFileW = new FileStream("C:\\CCListfile.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter CCListFileWriter = new StreamWriter(CCListFileW);
            CCListFileWriter.Flush();
            // ʹ��StreamWriter�����ļ���д������
            CCListFileWriter.BaseStream.Seek(0, SeekOrigin.Begin);
            // ��richTextBox1�е�����д���ļ�
            //Command += "exit\r\n";
            CCListFileWriter.Write(Command);
            //�رմ��ļ�;
            CCListFileWriter.Flush();
            CCListFileWriter.Close();

            Process CC = new Process();
            CC.StartInfo.FileName = "cmd.exe";
            CC.StartInfo.UseShellExecute = false;
            CC.StartInfo.RedirectStandardInput = true;
            CC.StartInfo.RedirectStandardOutput = true;
            CC.StartInfo.RedirectStandardError = true;
            CC.StartInfo.CreateNoWindow = true;

            CC.Start();
            CC.StandardInput.WriteLine("\"C:\\Program Files\\Incognito Software\\NT\\IMC\\ipcli.exe\" -N vice -p SQCX222 -S 192.168.222." + cccdhcp + " -F C:\\CCListfile.txt");
            CC.StandardInput.WriteLine("exit");

            string strRst = CC.StandardOutput.ReadToEnd();

            strInsert(mac, "ɾ��DHCP" + cccdhcp + "��Ȩ" + ccInfo, Form2.strName);


            MessageBox.Show("�����ɹ���");

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void label38_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
            
    }
   
}