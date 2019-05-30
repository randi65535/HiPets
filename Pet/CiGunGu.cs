using System.Collections.Generic;
using System.Xml;

namespace Pet
{
    /// <summary>
    /// 시군구 조회 오퍼레이션에서 받아온 결과 값을 각 개체에 저장하는 Class
    /// </summary>
    public class CiGunGu
    {
        #region 변수
        public string OrgCd { get; private set; }       //시군구코드
        public string OrgDownNm { get; private set; }   //시군구명
        public string UprCd { get; private set; }       //시군구상위코드
        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="orgcd">시군구코드</param>
        /// <param name="orgdownnm">시군구명</param>
        /// <param name="uprcd">시군구상위코드</param>
        public CiGunGu(string orgcd, string orgdownnm, string uprcd)
        {
            OrgCd = orgcd;
            OrgDownNm = orgdownnm;
            UprCd = uprcd;
        }

        /// <summary>
        /// XML에서 결과 값을 읽어오는 함수
        /// </summary>
        /// <param name="body_node">결과 값이 있는 XML 노드</param>
        /// <param name="animals">결과 값을 담아가는 자료구조</param>
        public static void Parsing(XmlNode body_node, List<CiGunGu> cigungues)
        {
            XmlNode items_node = body_node.SelectSingleNode("items");
            XmlNodeList item_nodes = items_node.SelectNodes("item");
            foreach (XmlNode snode in item_nodes)
            {
                string orgcd = snode.SelectSingleNode("orgCd").InnerText;
                string orgdownnm = snode.SelectSingleNode("orgdownNm").InnerText;
                string uprcd = snode.SelectSingleNode("uprCd").InnerText;
                CiGunGu cigungu = new CiGunGu(orgcd, orgdownnm, uprcd);
                cigungues.Add(cigungu);
            }
        }

        public override string ToString()
        {
            return OrgDownNm;
        }
    }
}