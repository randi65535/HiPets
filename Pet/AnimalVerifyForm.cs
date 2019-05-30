using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pet
{
    public partial class AnimalVerifyForm : Form
    {
        public Animal Animal { get; private set; }
        public AnimalVerifyForm()
        {
            InitializeComponent();
        }

        public AnimalVerifyForm(Animal animal)
        {
            Animal = animal;
            InitializeComponent();
        }

        private void HelloForm_Load(object sender, EventArgs e)
        {
            lbl_desertionNo.Text = Animal.CareNm;
            pb_animal.ImageLocation = Animal.PopfileUrl;
            lbl_happenPlace.Text = Animal.HappenPlace;
            lbl_happenDt.Text = Animal.HappenDt;
            lbl_kindCd.Text = Animal.KindCd;
            lbl_colorCd.Text = Animal.ColorCd;
            lbl_age.Text = Animal.Age;
            lbl_weight.Text = Animal.Weight;
            lbl_noticeNo.Text = Animal.NoticeNo;
            lbl_noticeSdt.Text = Animal.NoticeSdt;
            lbl_noticeEdt.Text = Animal.NoticeEdt;
            lbl_sexCd.Text = Animal.SexCd;
            lbl_neuterYn.Text = Animal.NeuterYn;
            lbl_specialMark.Text = Animal.SpecialMark;
            lbl_neuterYn.Text = Animal.NeuterYn;
            lbl_specialMark.Text = Animal.SpecialMark;
            lbl_careNm.Text = Animal.CareNm;
            lbl_careTel.Text = Animal.CareTel;
            lbl_careAddr.Text = Animal.CareAddr;
            lbl_orgNm.Text = Animal.OrgNm;
            lbl_chargeNm.Text = Animal.ChargeNm;
            lbl_officetel.Text = Animal.Officetel;
            lbl_noticeComment.Text = Animal.NoticeComment;
        }

    }
}
