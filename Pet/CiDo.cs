using System.Collections.Generic;
using System.Xml;

namespace Pet
{
    /// <summary>
    /// 시도 조회 오퍼레이션에서 받아온 결과 값을 각 개체에 저장하는 Class
    /// </summary>
    public class CiDo
    {
        #region 변수
        public string OrgCD { get; private set; }
        public string OrgDownNm { get; private set; }
        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="orgcd"></param>
        /// <param name="orgdownnm"></param>
        public CiDo(string orgcd, string orgdownnm)
        {
            OrgCD = orgcd;
            OrgDownNm = orgdownnm;
        }

        /// <summary>
        /// XML에서 결과 값을 읽어오는 함수
        /// </summary>
        /// <param name="body_node">결과 값이 있는 XML 노드</param>
        /// <param name="animals">결과 값을 담아가는 자료구조</param>
        public static void Parsing(XmlNode body_node, List<CiDo> cidoes)
        {
            XmlNode items_node = body_node.SelectSingleNode("items");
            XmlNodeList item_nodes = items_node.SelectNodes("item");

            foreach (XmlNode snode in item_nodes)
            {
                string orgcd = snode.SelectSingleNode("orgCd").InnerText;
                string orgdownnm = snode.SelectSingleNode("orgdownNm").InnerText;

                CiDo cido = new CiDo(orgcd, orgdownnm);
                cidoes.Add(cido);
            }
        }

        public override string ToString()
        {
            return OrgDownNm;
        }
    }
}