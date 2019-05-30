using System.Collections.Generic;
using System.Xml;

namespace Pet
{
    /// <summary>
    /// 보호소 조회 오퍼레이션에서 받아온 결과 값을 각 개체에 저장하는 Class
    /// </summary>
    public class Care
    {
        #region 변수
        public string CareNm { get; private set; }      //
        public string CareRegNo { get; private set; }   //
        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="carenm">보호소명</param>
        /// <param name="careregno">보호소번호</param>
        public Care(string carenm, string careregno)
        {
            CareNm = carenm;
            CareRegNo = careregno;
        }

        /// <summary>
        /// XML에서 결과 값을 읽어오는 함수
        /// </summary>
        /// <param name="body_node">결과 값이 있는 XML 노드</param>
        /// <param name="animals">결과 값을 담아가는 자료구조</param>
        public static void Parsing(XmlNode body_node, List<Care> cares)
        {
            XmlNode items_node = body_node.SelectSingleNode("items");
            XmlNodeList item_nodes = items_node.SelectNodes("item");
            foreach (XmlNode snode in item_nodes)
            {
                string carenm = snode.SelectSingleNode("careNm").InnerText;
                string careregno = snode.SelectSingleNode("careRegNo").InnerText;

                Care care = new Care(carenm, careregno);
                cares.Add(care);
            }
        }

        public override string ToString()
        {
            return CareNm;
        }
    }
}