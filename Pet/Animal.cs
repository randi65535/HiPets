using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Pet
{
    /// <summary>
    /// 유기동물 조회 오퍼레이션에서 받아온 결과 값을 각 개체에 저장하는 Class
    /// </summary>
    public class Animal
    {
        #region 변수
        public string DesertionNo { get; private set; }     //유기번호
        public string ThumbnailUrl { get; private set; }    //썸네일 이미지 경로
        public string PopfileUrl { get; private set; }      //이미지 경로
        public string HappenPlace { get; private set; }     //발견 장소
        public string HappenDt { get; private set; }        //접수일
        public string KindCd { get; private set; }          //품종
        public string ColorCd { get; private set; }         //색상
        public string Age { get; private set; }             //나이
        public string Weight { get; private set; }          //체중
        public string NoticeNo { get; private set; }        //공고번호
        public string NoticeSdt { get; private set; }       //공고시작일
        public string NoticeEdt { get; private set; }       //공고종료일
        public string SexCd { get; private set; }           //성별
        public string NeuterYn { get; private set; }        //중성화 여부
        public string SpecialMark { get; private set; }     //특징
        public string CareNm { get; private set; }          //보호소이름
        public string CareTel { get; private set; }         //보호소전화번호
        public string CareAddr { get; private set; }        //보호장소
        public string OrgNm { get; private set; }           //관할기관
        public string ChargeNm { get; private set; }        //담당자
        public string Officetel { get; private set; }       //담당자 연락처
        public string NoticeComment { get; private set; }   //특이사항
        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="desertionNo">유기번호</param>
        /// <param name="thumbnailUrl">썸네일 이미지 경로</param>
        /// <param name="popfileUrl">이미지 경로</param>
        /// <param name="happenPlace">발견 장소</param>
        /// <param name="happenDt">접수일</param>
        /// <param name="kindCd">품종</param>
        /// <param name="colorCd">색상</param>
        /// <param name="age">나이</param>
        /// <param name="weight">체중</param>
        /// <param name="noticeNo">공고번호</param>
        /// <param name="noticeSdt">공고시작일</param>
        /// <param name="noticeEdt">공고종료일</param>
        /// <param name="sexCd">성별</param>
        /// <param name="neuterYn">중성화 여부</param>
        /// <param name="specialMark">특징</param>
        /// <param name="careNm">보호소이름</param>
        /// <param name="careTel">보호소전화번호</param>
        /// <param name="careAddr">보호장소</param>
        /// <param name="orgNm">관할기관</param>
        /// <param name="chargeNm">담당자</param>
        /// <param name="officetel">담당자연락처</param>
        /// <param name="noticeComment">특이사항</param>
        public Animal
        (
        string desertionNo, string thumbnailUrl, string popfileUrl, string happenPlace, string happenDt,
        string kindCd, string colorCd, string age, string weight, string noticeNo,
        string noticeSdt, string noticeEdt, string sexCd, string neuterYn, string specialMark,
        string careNm, string careTel, string careAddr, string orgNm, string chargeNm,
        string officetel, string noticeComment
        )
        {
            DesertionNo = desertionNo;
            ThumbnailUrl = thumbnailUrl;
            PopfileUrl = popfileUrl;
            HappenPlace = happenPlace;
            HappenDt = happenDt;
            KindCd = kindCd;
            ColorCd = colorCd;
            Age = age;
            Weight = weight;
            NoticeNo = noticeNo;
            NoticeSdt = noticeSdt;
            NoticeEdt = noticeEdt;
            SexCd = sexCd;
            NeuterYn = neuterYn;
            SpecialMark = specialMark;
            CareNm = careNm;
            CareTel = careTel;
            CareAddr = careAddr;
            OrgNm = orgNm;
            ChargeNm = chargeNm;
            Officetel = officetel;
            NoticeComment = noticeComment;
        }

        /// <summary>
        /// XML에서 결과 값을 읽어오는 함수
        /// </summary>
        /// <param name="body_node">결과 값이 있는 XML 노드</param>
        /// <param name="animals">결과 값을 담아가는 자료구조</param>
        public static void Parsing(XmlNode body_node, List<Animal> animals)
        {
            XmlNode items_node = body_node.SelectSingleNode("items");
            XmlNodeList item_nodes = items_node.SelectNodes("item");
            foreach (XmlNode snode in item_nodes)
            {
                #region 파싱 변수들
                string desertionNo = snode.SelectSingleNode("desertionNo").InnerText;
                string thumbnailUrl = snode.SelectSingleNode("filename").InnerText;
                string popfileUrl = snode.SelectSingleNode("popfile").InnerText;
                string happenPlace = snode.SelectSingleNode("happenPlace").InnerText;
                string happenDt = snode.SelectSingleNode("happenDt").InnerText;
                string kindCd = snode.SelectSingleNode("kindCd").InnerText;
                string colorCd = snode.SelectSingleNode("colorCd").InnerText;
                string age = snode.SelectSingleNode("age").InnerText;
                string weight = snode.SelectSingleNode("weight").InnerText;
                string noticeNo = snode.SelectSingleNode("noticeNo").InnerText;
                string noticeSdt = snode.SelectSingleNode("noticeSdt").InnerText;
                string noticeEdt = snode.SelectSingleNode("noticeEdt").InnerText;
                string sexCd = snode.SelectSingleNode("sexCd").InnerText;
                string neuterYn = snode.SelectSingleNode("neuterYn").InnerText;
                string specialMark = snode.SelectSingleNode("specialMark").InnerText;
                string careNm = snode.SelectSingleNode("careNm").InnerText;
                if(careNm == string.Empty) { careNm = "보호자 없음"; }

                string careTel = snode.SelectSingleNode("careTel").InnerText;
                string careAddr = snode.SelectSingleNode("careAddr").InnerText;
                string orgNm = snode.SelectSingleNode("orgNm").InnerText;

                XmlNode chargeNmNode = snode.SelectSingleNode("chargeNm");
                string chargeNm = string.Empty;
                if (chargeNmNode != null)
                {
                    chargeNm = snode.SelectSingleNode("chargeNm").InnerText;
                }

                string officetel = snode.SelectSingleNode("officetel").InnerText;

                XmlNode noticeCommentNode = snode.SelectSingleNode("noticeComment");
                string noticeComment = string.Empty;
                if (noticeCommentNode != null)
                {
                    noticeComment = snode.SelectSingleNode("noticeComment").InnerText;
                }
                #endregion
                Animal animal = new Animal
                (
                    desertionNo, thumbnailUrl, popfileUrl, happenPlace, happenDt,
                    kindCd, colorCd, age, weight, noticeNo,
                    noticeSdt, noticeEdt, sexCd, neuterYn, specialMark,
                    careNm, careTel, careAddr, orgNm, chargeNm,
                    officetel, noticeComment
                );

                animals.Add(animal);
            }
        }

        private void FillDataFromXmlToString(XmlNode singleNode, string inputStr)
        {
            // new NotImplementedException();
        }

        public override string ToString()
        {
            return DesertionNo;
        }
    }
}
