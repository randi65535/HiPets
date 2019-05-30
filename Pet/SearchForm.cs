using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// 최종 수정일 : 2019.05.22
/// </summary>
namespace Pet
{

    public partial class SearchForm : Form
    {
        public SearchForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 폼 생성시 초기화하는 함수
        /// </summary>
        private void SearchForm_Load(object sender, EventArgs e)
        {
            InitCidoList();
            InitSearchArgs();
            InitDateTimePicker();

            tlp_animals.Width = splitContainer3.Width;
        }


        /// <summary>
        /// 검색시 필요한 변수들 초기화 하는 함수
        /// </summary>
        private void InitSearchArgs()
        {
            #region 검색시 필요한 변수
            upkind = string.Empty;
            kind = string.Empty;
            upr_cd = string.Empty;
            org_cd = string.Empty;
            care_reg_no = string.Empty;
            state = string.Empty;
            neuter_yn = string.Empty;
            #endregion
        }

        /// <summary>
        /// 왼쪽 달력의 값을 임의로 지정해주는 함수
        /// </summary>
        private void InitDateTimePicker()
        {
            DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            dtp_start.Value = today.AddDays(-7);
        }

        /// <summary>
        /// 시작시 시도에 대한 정보들을 리스트박스에 초기화 하는 함수
        /// </summary>
        private void InitCidoList()
        {
            List<CiDo> cidoes = AniParser.FindCidoList();

            foreach (CiDo cido in cidoes)
            {
                lbox_cido.Items.Add(cido);
            }
        }

        /// <summary>
        /// 시도를 누르면 시군구 리스트를 불러오는 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbox_cido_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbox_cido.SelectedItem == null)
            {
                return;
            }
            lbox_cgg.Items.Clear();
            lbox_care.Items.Clear();

            CiDo cido = lbox_cido.SelectedItem as CiDo;
            upr_cd = cido.OrgCD;

            // 시도를 골랐으니 다음 선택지들인 시군구 변수와 보호소 번호 변수를 비운다.
            org_cd = string.Empty;
            care_reg_no = string.Empty;

            List<CiGunGu> cigungues = AniParser.FindCiGunGuList(cido);
            foreach (CiGunGu cgg in cigungues)
            {
                lbox_cgg.Items.Add(cgg);
            }
        }

        /// <summary>
        /// 시군구를 누르면 보호소 리스트가 나오는 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbox_cgg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbox_cgg.SelectedItem == null)
            {
                return;
            }

            lbox_care.Items.Clear();

            CiGunGu cgg = lbox_cgg.SelectedItem as CiGunGu;
            org_cd = cgg.OrgCd;
            // 시군구를 골랐으니 다음 선택지인 보호소 번호를 비운다.
            care_reg_no = string.Empty;

            List<Care> cares = AniParser.FindCareList(cgg);
            foreach (Care care in cares)
            {
                lbox_care.Items.Add(care);
            }
        }

        /// <summary>
        /// 동물들의 품종을 클릭했을 때 변수에 값을 넣어주는 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbox_care_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbox_care.SelectedItem == null)
            {
                return;
            }

            Care care = (Care)lbox_care.SelectedItem;
            care_reg_no = care.CareRegNo;
        }

        /// <summary>
        /// 축종정보를 클릭하면 품종정보를 불러오는 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_kindCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_kindCode.SelectedItem == null){ return; }

            switch (cb_kindCode.SelectedItem.ToString())
            {
                #region switch 문
                // 개의 축종코드 417000
                case "개":
                    upkind = "417000";
                    break;

                // 고양이의 축종코드 422400
                case "고양이":
                    upkind = "422400";
                    break;

                // 기타의 축종코드 429900
                case "기타":
                    upkind = "429900";
                    break;
                default:
                    upkind = string.Empty;
                    break;
                #endregion
            }
            
            List<Kind> kinds = new List<Kind>();
            kinds = AniParser.FindKindList(upkind);
            InitKindList(kinds);
        }

        /// <summary>
        /// 콤보박스에서 개, 고양이, 기타를 클릭시 lbox_kind에 품종에 관한 정보를 넣어주는 함수
        /// </summary>
        /// <param name="kinds"></param>
        private void InitKindList(List<Kind> kinds)
        {
            lbox_kind.Items.Clear();
            foreach (Kind kind in kinds)
            {
                lbox_kind.Items.Add(kind);
            }
        }

        /// <summary>
        /// 품종정보를 클릭시 품종 정보를 저장하는 함수
        /// </summary>
        /// <param name="kinds"></param>
        private void lbox_kind_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbox_kind.SelectedItem == null)
            {
                return;
            }

            Kind oKind = (Kind)lbox_kind.SelectedItem;
            kind = oKind.KindCd; //축종코드를 대입해주는 곳
        }

        private string upkind;      // cb_kindCode_SelectedIndexChanged 에서 대입
        private string kind;        // lbox_kind_SelectedIndexChanged 에서 대입
        private string upr_cd;      // lbox_cgg_SelectedIndexChangedd 에서 대입
        private string org_cd;      // lbox_cido_SelectedIndexChanged 에서 대입
        private string care_reg_no; // lbox_care_SelectedIndexChanged 에서 대입
        private string state;       // MakeRequestAnimal에서 대입 // 미설정
        private string neuter_yn;   // MakeRequestAnimal에서 대입 // 미설정
        private void btn_search_Click(object sender, EventArgs e)
        {

            string bgnde = dtp_start.Value.ToString("yyyyMMdd");
            string endde = dtp_end.Value.ToString("yyyyMMdd");

            int isValidDateSetting = endde.CompareTo(bgnde);
            if (isValidDateSetting <= -1)
            {
                MessageBox.Show("왼쪽 날짜는 오른쪽 날짜보다 같거나 작아야 합니다.", "경고");
                _lbl_animals_count.Visible = false;
                return;
            }

            tlp_animals.Controls.Clear();
            tlp_animals.RowStyles.Clear();

            int.TryParse(tb_count.Text.Trim(), out int numOfRows);
            //요청을 할 자료구조 구함
            Dictionary<string, string> requestAnimal = new Dictionary<string, string>();
            MakeRequestAnimal(requestAnimal, bgnde, endde, numOfRows, upkind, kind, upr_cd, org_cd, care_reg_no, state, neuter_yn);

            List<Animal> animals = AniParser.FindAnimalList(requestAnimal);

            if (animals.Count == 0)
            {
                _lbl_animals_count.Visible = false;
                lbl_animals_count.Text = "검색 결과가 없습니다.";
                return;
            }

            _lbl_animals_count.Visible = true;
            lbl_animals_count.Text = animals.Count.ToString();

            FillDataTLP(animals, mainSpc.Width / 2, mainSpc.Panel2.Height);
            
        }

        /// <summary>
        /// Url을 만들기 위해 변수를 세팅하는 함수
        /// </summary>
        /// <param name="requestAnimal">세팅하기 전 변수를 가지고 있는 자료구조</param>
        /// <param name="bgnde">공고 시작일</param>
        /// <param name="endde">공고 종료일</param>
        /// <param name="numOfRows">한 페이지당 검색 개수</param>
        /// <param name="upkind">축종코드</param>
        /// <param name="kind">품종코드</param>
        /// <param name="upr_cd">시도</param>
        /// <param name="org_cd">시군구</param>
        /// <param name="care_reg_no">보호소</param>
        /// <param name="state">상태</param>
        /// <param name="neuter_yn">중성화여부</param>
        private void MakeRequestAnimal(Dictionary<string, string> requestAnimal, string bgnde, string endde, int numOfRows, string upkind, string kind, string upr_cd, string org_cd, string care_reg_no, string state, string neuter_yn)
        {

            if (bgnde != string.Empty) { requestAnimal["bgnde"] = bgnde;
            } else{ requestAnimal["bgnde"] = string.Empty; }

            if (endde != string.Empty) { requestAnimal["endde"] = endde;
            } else { requestAnimal["endde"] = string.Empty; }

            if (numOfRows <= 0) { numOfRows = 30; }
            requestAnimal["numOfRows"] = numOfRows.ToString();

            if (upkind != string.Empty) { requestAnimal["upkind"] = upkind; }
            else { requestAnimal["upkind"] = string.Empty; }

            if (kind != string.Empty) { requestAnimal["kind"] = kind; }
            else { requestAnimal["kind"] = string.Empty; }

            if (upr_cd != string.Empty){ requestAnimal["upr_cd"] = upr_cd; }
            else{ requestAnimal["upr_cd"] = string.Empty; }

            if (org_cd != string.Empty){ requestAnimal["org_cd"] = org_cd; }
            else{ requestAnimal["org_cd"] = string.Empty; }

            if (care_reg_no != string.Empty) { requestAnimal["care_reg_no"] = care_reg_no; }
            else { requestAnimal["care_reg_no"] = string.Empty; }

            if (state != string.Empty) {requestAnimal["state"] = state; }
            else { requestAnimal["state"] = string.Empty; }

            if (neuter_yn != string.Empty) { requestAnimal["neuter_yn"] = neuter_yn; }
            else { requestAnimal["neuter_yn"] = string.Empty; }

        }

        /// <summary>
        /// TableLayoutPanel을 채우는 함수
        /// </summary>
        /// <param name="animals">개체들을 가지고 있는 리스트</param>
        /// <param name="tlpCellWidth">TableLayoutPanel의 Cell Width</param>
        /// <param name="tlpCellHeight">TableLayoutPanel의 Cell Height</param>
        private void FillDataTLP(List<Animal> animals, int tlpCellWidth, int tlpCellHeight)
        {
            for (int i = 0; i < animals.Count; i++)
            {
                Animal animal = animals[i] as Animal;

                Panel panel = new Panel()
                {
                    #region Panel 세팅
                    Width = tlpCellWidth,
                    Height = tlpCellHeight,
                    Margin = new Padding(0),
                    Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left,
                    #endregion
                };

                Label lbl = new Label()
                {
                    #region Label 세팅
                    Width = tlpCellWidth,
                    Text = string.Format("{0}", animal.CareAddr),
                    Anchor = AnchorStyles.Left | AnchorStyles.Top,
                    #endregion
                };
                panel.Controls.Add(lbl);

                PictureBox pb = new PictureBox()
                {
                    #region PictureBox 세팅
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    ImageLocation = animal.PopfileUrl,
                    Dock = DockStyle.Fill,
                    Padding = new Padding(0),
                    Margin = new Padding(0),
                    #endregion
                };
                pb.Click += new EventHandler(pb_click);
                pb.Tag = animal; //사진 클릭시 상세보기를 위해서 정보를 개체를 Picture 박스에 저장

                panel.Controls.Add(pb);
                tlp_animals.Controls.Add(panel);
            }

        }

        AnimalVerifyForm avf;
        /// <summary>
        /// 사진 클릭시 폼이 나타나는 이벤트 처리 함수
        /// </summary>
        /// <param name="sender">PictureBox</param> : Tag에 Animal 개체가 저장되어 있다.
        /// <param name="e"></param> 
        private void pb_click(object sender, EventArgs e)
        {
            if (avf == null)
            {
                Control ctrl = sender as Control;
                Animal animal = ctrl.Tag as Animal;

                //폼 종료시 null 개체로 만들어 줌   
                avf = new AnimalVerifyForm(animal);
                avf.FormClosed += OtherForm_Closed;
                avf.ShowDialog();
            }
        }

        /// <summary>
        /// 폼이 닫혔을 때 null로 만들어 주는 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OtherForm_Closed(object sender, EventArgs e)
        {
            if (sender is AnimalVerifyForm) avf = null;
        }

        /// <summary>
        /// 해당 창을 닫는 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 해당 폼의 사이즈가 변경시 발생하는 이벤트
        /// </summary>
        private void SearchForm_SizeChanged(object sender, EventArgs e)
        {
            //크기 확대시 Panel의 Width와 Height를 수정
            tlp_animals.Width = mainSpc.Width; 
            tlp_animals.Height = mainSpc.Panel2.Height;
        }

    }
}
