using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Pet
{
    static public class AniParser
    {

        private const string READ_SUCCESS = "00";

        /// <summary>
        /// 시도를 찾는 함수
        /// </summary>
        /// <returns></returns>
        public static List<CiDo> FindCidoList()
        {
            List<CiDo> cidoes = new List<CiDo>();
            string url = string.Format("http://openapi.animal.go.kr/openapi/service/rest/abandonmentPublicSrvc/sido?numOfRows={0}&ServiceKey={1}", 17, Properties.Settings.Default.SvcKey);

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(url);

            XmlNode res_node = xdoc.SelectSingleNode("response");
            XmlNode head_node = res_node.SelectSingleNode("header");
            XmlNode result_node = head_node.SelectSingleNode("resultCode");

            string result = result_node.InnerText;
            if (result != "00")
            {
                return cidoes;
            }
            CiDo.Parsing(res_node.SelectSingleNode("body"), cidoes);
            return cidoes;
        }

        /// <summary>
        /// 시군구 정보를 찾는 함수
        /// </summary>
        /// <param name="cido">시도 지역(상위 지역)</param>
        /// <returns>시군구 리스트</returns>
        public static List<CiGunGu> FindCiGunGuList(CiDo cido)
        {
            List<CiGunGu> cigungues = new List<CiGunGu>();
            string url = string.Format("http://openapi.animal.go.kr/openapi/service/rest/abandonmentPublicSrvc/sigungu?upr_cd={0}&ServiceKey={1}", cido.OrgCD, Properties.Settings.Default.SvcKey);

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(url);

            XmlNode res_node = xdoc.SelectSingleNode("response");
            XmlNode head_node = res_node.SelectSingleNode("header");
            XmlNode result_node = head_node.SelectSingleNode("resultCode");

            string result = result_node.InnerText;
            if (result != READ_SUCCESS)
            {
                return cigungues;
            }

            CiGunGu.Parsing(res_node.SelectSingleNode("body"), cigungues);
            return cigungues;

        }

        /// <summary>
        /// 보호소 정보를 찾는 함수
        /// </summary>
        /// <param name="cgg">시군구 지역(상위 지역)</param>
        /// <returns></returns>
        public static List<Care> FindCareList(CiGunGu cgg)
        {
            string url = string.Format("http://openapi.animal.go.kr/openapi/service/rest/abandonmentPublicSrvc/shelter?upr_cd={0}&org_cd={1}&ServiceKey={2}", cgg.UprCd, cgg.OrgCd, Properties.Settings.Default.SvcKey);
            List<Care> cares = new List<Care>();

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(url);

            XmlNode res_node = xdoc.SelectSingleNode("response");
            XmlNode head_node = res_node.SelectSingleNode("header");
            XmlNode result_node = head_node.SelectSingleNode("resultCode");

            string result = result_node.InnerText;
            if (result != READ_SUCCESS)
            {
                return cares;
            }

            Care.Parsing(res_node.SelectSingleNode("body"), cares);
            return cares;

        }

        public static List<Kind> FindKindList(string kindCode)
        {
            string url = string.Format("http://openapi.animal.go.kr/openapi/service/rest/abandonmentPublicSrvc/kind?up_kind_cd={0}&ServiceKey={1}", kindCode, Properties.Settings.Default.SvcKey);
            List<Kind> kinds = new List<Kind>();

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(url);

            XmlNode res_node = xdoc.SelectSingleNode("response");
            XmlNode head_node = res_node.SelectSingleNode("header");
            XmlNode result_node = head_node.SelectSingleNode("resultCode");

            string result = result_node.InnerText;
            if (result != READ_SUCCESS)
            {
                return kinds;
            }

            Kind.Parsing(res_node.SelectSingleNode("body"), kinds);
            return kinds;
        }

        /// <summary>
        /// 유기된 동물들을 찾는 함수
        /// </summary>
        /// <param name="requestAnimal">요청에 필요한 변수</param>
        /// <returns>유기된 동물 리스트</returns>
        public static List<Animal> FindAnimalList(Dictionary<string, string> requestAnimal)
        {
            
           string url = MakeUrl(requestAnimal);

            List<Animal> animals = new List<Animal>();

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(url);

            XmlNode res_node = xdoc.SelectSingleNode("response");
            XmlNode head_node = res_node.SelectSingleNode("header");
            XmlNode result_node = head_node.SelectSingleNode("resultCode");

            string result = result_node.InnerText;
            if (result != READ_SUCCESS)
            {
                return animals;
            }

            Animal.Parsing(res_node.SelectSingleNode("body"), animals);

            return animals;
        }

        private static string[] REQ_ARGS = new string[] {"bgnde", "endde", "upkind", "kind", "upr_cd", "org_cd","care_reg_no", "state", "neuter_yn", "numOfRows" };
        private static string MakeUrl(Dictionary<string, string> requestAnimal)
        {
            StringBuilder sb = new StringBuilder("http://openapi.animal.go.kr/openapi/service/rest/abandonmentPublicSrvc/abandonmentPublic?");
            sb.AppendFormat("{0}={1}&", "ServiceKey", Properties.Settings.Default.SvcKey);
            for (int i = 0; i < REQ_ARGS.Length; i++)
            {
                string value = REQ_ARGS[i];

                 if (requestAnimal[value] != string.Empty && requestAnimal[value] != null)
                {
                    sb.AppendFormat("{0}={1}&", value, requestAnimal[value]);
                }

            }

            return sb.ToString();
        }
    }
}
