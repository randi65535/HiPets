using System.Collections.Generic;
using System.Xml;

namespace Pet
{
    /// <summary>
    /// 품종 조회 오퍼레이션 명세에서 받아온 결과 값을 각 개체에 저장하는 Class
    /// </summary>
    public class Kind
    {
        #region 변수
        public string KNm { get; private set; }     //품종명
        public string KindCd { get; private set; }  //품종코드
        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="knm">품종명</param>
        /// <param name="kindcd">품종코드</param>
        public Kind(string knm, string kindcd)
        {
            KNm = knm;
            KindCd = kindcd;
        }

        /// <summary>
        /// XML에서 결과 값을 읽어오는 함수
        /// </summary>
        /// <param name="body_node">결과 값이 있는 XML 노드</param>
        /// <param name="animals">결과 값을 담아가는 자료구조</param>
        public static void Parsing(XmlNode body_node, List<Kind> kinds)
        {
            XmlNode items_node = body_node.SelectSingleNode("items");
            XmlNodeList item_nodes = items_node.SelectNodes("item");
            foreach (XmlNode snode in item_nodes)
            {
                string KNm = snode.SelectSingleNode("KNm").InnerText; // 개 이름
                string kindCd = snode.SelectSingleNode("kindCd").InnerText; // 상세 품종 코드
                Kind kind = new Kind(KNm, kindCd);
                kinds.Add(kind);
            }

        }
        public override string ToString()
        {
            return KNm;
        }
    }
}