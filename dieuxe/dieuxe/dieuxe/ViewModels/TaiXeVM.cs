using Android.Content;
using dieuxe.Helpers;
using dieuxe.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dieuxe.ViewModels
{
    public class TaiXeVM:INotifyPropertyChanged
    {
       private List<Dieuxe> chuyenchuadi = new List<Dieuxe>();
        private List<Dieuxe> chuyendadi = new List<Dieuxe>();
        public List<Dieuxe> DanhSachChuyenChuaDi 
        { 
            get { return chuyenchuadi; }
            set
            {
                chuyenchuadi = value;
                OnPropertyChanged();
            }
        }
        public List<Dieuxe> DanhSachChuyenDaDi
        {
            get { return chuyendadi; }
            set
            {
                chuyendadi = value;
                OnPropertyChanged();
            }
        }
        private bool _laydulieuxong;
        public bool checkdata
        {
            get { return _laydulieuxong; }
            set { _laydulieuxong = value;
                OnPropertyChanged();
            }
        }
        public TaiXeVM()
        {
            //Laydulieu();

            chuyenchuadi.Add(new Dieuxe { MaDieuxe = 1, SoNguoi = 7, NoiDi = "48 tăng nhơn phú", NoiDen = "243 Phan Huy Ích, Phường 12, Gò Vấp", TuyenDuongDi = "mccaAo{_jSKRKXKZIPUt@Wt@Uv@GR[lA?BI\\WfAEXWrAShAMv@O~@Kj@M|@Ib@a@tCa@nCCLGf@CLCRa@zBWtAg@tC_@dCMv@StAUrAMt@WpAUzAQxAQdAIl@Ox@Ij@UvAYbBUtAi@`D_@`CSzAShAKb@SjAAJG\\ENSXMT[b@mAvA_@f@U\\[`@c@f@QV_@h@EFiAdBU`@Yd@MTmArBU^SVKRc@x@i@|@iAlBS^e@t@]j@g@z@i@z@Yb@gAfB", TuyenDuongVe = "ihcaA}eziSkLgE_C{@F`Av@pJB^C_@]oDa@cGaKsDkDuA}@k@gBiAmFuCcFiC[Un@cAtBkDpA{BjDaGlGcKfBcCjDoEj@{@`@uBn@kEfDeThFq\\lAyG^_ChAoHNaA`AeFbAcFhAaDj@{A@EQAmBo@g@G_@Ds@@mFs@oBUsEk@?yCFcTB}IBmMQA?l@AdCAhBAtD?zB~A?hEFfCAvF?tFDxBJFUzAeGbCgJ`EcPnAeFzBwAlKgFHKv@a@jCmAh@Ur@Ox@Ex@DlIzBHD@DZ?fB\\z@H~DZ`JJfE@pHFlB@rBFzBDp@Ex@AjCBzAB|@H`Ah@bAr@xFfEl@j@D\\nE|CpAhA|@~@RORUz@}@bBiBxAwAjB_BzB}B`Ak@FMBu@yBAMQGMz@}AXk@\\WbAu@n@gAfBgCdCmDlDyEjBqC`@g@x@s@dDaCsAwAa@i@Us@Gy@x@_NVeDj@_KQAEp@GfAa@@e@@d@A`@ASnDa@zFy@zMF`AVv@hAvAj@n@jDjDrBxB`B`BnJxJr@rADl@YpHMxDlBK`FOhIShJS\\AXu@Xq@nFkK|AcDXY`AiAhAqA|BkCnCuCr@cAvCcD~AgBzC}CbFwFnFcG|AcBrJ{KdHoIzEiFnDkEbEeEpA_BfBqBrDaE`EwE`Cw@xH_CvCcAlJyCzJcD}BwGm@cBnBq@jAa@gAsDUeCQk@WS{@W_B[e@DSFyBIwAEw@AgJ_@cEKuBIkBGgAK{Es@wJcByE_Ak@CUDi@b@gBrBmDjDOQeEmD_F_EoBoA}LuDeCu@yAi@{@k@u@u@w@sAQm@KgAAiA^cGVeCVaBJuBBiBI}A[kAaAsBcAwBo@y@w@i@g@U_AW}BQyF@mHGaCEaAQq@Ug@[y@u@a@i@gD{EoAmBc@q@YS?}@MAD}@IiCoAyL}BcSmC_Wc@iEs@aEi@mEeDsZ{CuYi@eFE}AF_BDoBEm@Ko@Og@_@y@k@qBk@{Cw@wEi@_CaBqEmAoC_@q@eCwDuB_CeC}BqDgC]QkCuAuBy@sDmAq@Q{Cm@mDg@iCW}Gs@cD[{N{A{IkAgBSiRcCYU_Cs@qGgCyIeEoAi@\\cBJi@NoANyBBqAIgCAyB`@{CV{Ab@wBz@}A~@sAPg@^sAr@iDLaBeCZ{DZyEb@Iu@McBHq@LaAI{@SgB[q@kAeCu@qAuA}@aAu@yBoCeEwEmA{AcByBi@u@Ok@Ec@Mg@g@m@aByBDc@GYGk@DMJAV@", GioDi = "8:00 20/2/2020", GioVe = "17:00 20/2/2020", TaiXe = 1, BienKiemSoat = "51A - 1223", TrangThai = 1, GhiChu = "", DsDiemDung = "wtbaAwavjS~iK~xM_sBrcBcdBiWoyE~kBjCfnFtOaE" });
            chuyenchuadi.Add(new Dieuxe { MaDieuxe = 2, SoNguoi = 2, NoiDi = "48 tăng nhơn phú", NoiDen = "điểm cuối chưa biết", TuyenDuongDi = "ytbaAoavjS_@AEFCNJl@BRAVCF{@u@UIo@Oa@QOMIOEQ?SHs@RaAFk@CYe@_Bi@{AkAqDa@}@W[[SaB}@a@Wo@i@]k@z@EfACVGlC{AXUdAiATYJGRK^GtAIp@CrACdB?j@NnAb@d@RbA{CzAsFdAwEp@qDZ}BHa@Na@`@]dBcAREPDd@D~A`@h@Ff@@LEtEyCbBeAhMyI|HmF^WQUqCqEeBuCsBsCyMeSyC}EuG}Jc@o@{CuEuC}DsAgB{BsBUUsDaC{@i@mEkCwC}AaFyC}FaEaCgB{AkAkCeCaBaBUa@Q_@Ku@IOOMOKSCc@BYROZC\\@J_@v@mGdHuN`OoKjLkAtA@IAQIUOQCA", GioDi = "8:00 20/2/2020", GioVe = "8:00 20/2/2020", TaiXe = 1, BienKiemSoat = "51A - 1223", TrangThai = 0, GhiChu = "", DsDiemDung = "ytbaAoavjSctAufD" });
            chuyenchuadi.Add(new Dieuxe { MaDieuxe = 3, SoNguoi = 333, NoiDi = "vĩnh lộc a", NoiDen = "1 quách điêu", TuyenDuongDi = "ytbaAoavjS_@AIVN`AAVCF\\`@z@lAb@f@P^Lx@Rt@pCpD`H~HhB~BbBfAr@j@v@zAxAzCNr@JnAF|@UhA?f@T`C?F}@BaBDaADoANwB`@gBh@_Bd@eAPiAHm@Fw@T]Pm@f@aA^e@HmAJ{ACoBIe@BiA^mA~@qAjA_A`Bo@bAiDcBeHcDa@@oAVm@Ls@Zk@R}ClA{@h@[f@yC`F{@vAg@x@]^_@\\{BlA}BdAQJ{BoD_@g@o@eA]s@a@kAYy@e@m@UQWCu@@uCNe@EeJiEaCcAqFmCh@_@^a@LYFUiCw@cD}@yGkB{Ag@o@e@eBsAg@SD{CZwC?o@I[aAqCOm@C]e@K_@Oo@o@wDyH}A}CgAqB_@N}@XqBv@{@^{@P{@V[@_ABeDTcAdDYnAQzB[lFMfACTqJkD_QeGsI_DiAa@_Aa@_A_@qGgCv@wCvBsJfCeL|@}Dx@eE\\mDPuDLmJ@eA?sCEUCc@W?AN@pDOrIGbDKzBKtAYvBqB`JgAfFwBrJwCzLOr@QIW?QEqBo@_@IwApAeCrEw@xAwAlC_AdC_AlCiB|E}BbCwArAoBfEu@hAmBdCYz@YfAQZk@C}AEmBKcB_@a@Oo@[y@o@yBqBiA{@WUMRe@Rj@vDh@pCnAxFhChKfAhEJb@}Al@yCjAUJIBIDcC~AyAx@aElAaC^}CX_BEeDLiBFcC@oBGnBFbCAhBGdDM~AD|CY`C_@`EmAxAy@n@a@|AcAxD{A|Am@r@hCb@bB`AzDrAhF`B~GdA~E|AvHPRC^AvAF|ADj@j@`Dl@hCRz@`G~WjBjJb@~Af@jBtD|Pj@hEb@pDf@vHJpAj@xEXhBbAzD|D`KtBvFlE|KjCrFvBbFpBrErChHlDbJ|EjMxJ`VlJ~T`D|Hx@|Bj@zCFhABtCU~E_AzNeAxOcAvOsA~Sa@zHMzFWfJe@vOeA|a@MzEQrFWf@[Lc@BkH]mBIYBSLOb@GvAApAFZFPHNh@@`Kp@|@Fx@DzBLdJd@dBNvBLzHf@dIf@hCTfD^vK|@xEX|IP\\?`@?tBNxAThCn@dIzBzDz@|Cj@hDtAzEv@pInB\\Hv@\\`@HEp@@`J?z@C`FApME~UAvCrBVnEj@nD`@nBVd@IZ?tA^n@TP@vCz@rAT~AC`A?vAP|Cf@tBZH@DKFo@DMl@k@ZErBE?n@@dAAZEAc@@[D_@ZiCjDe@p@Wf@WBs@j@sBtBm@r@o@|AO^Y`Au@rBiCrC{BbCiDhDp@d@IRSb@Mf@]hBg@jCGp@DTQJIJK`@MJ]AMDGP", GioDi = "", GioVe = "", TaiXe = 1, BienKiemSoat = "51A - 1223", TrangThai = 0, GhiChu = "", DsDiemDung = "olcaAoskiSR?Bd@AbA" });

        }

        public async Task Laydulieu()
        {
            var accessToken = Settings.AccessToken;
           await GetDadi("https://apidieuxe.azurewebsites.net/api/getLichDadicuaTaixe", accessToken);
           await  GetChuadi("https://apidieuxe.azurewebsites.net/api/getLichChuadicuaTaixe", accessToken);
            if(chuyenchuadi!=null && chuyendadi != null)
            {
                checkdata = true;
            }
        }

        public async Task  GetDadi(string url, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await client.GetAsync(url);

            System.Diagnostics.Debug.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var st = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                DanhSachChuyenDaDi = JsonConvert.DeserializeObject<List<Dieuxe>>(content,st);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("error", "you should be login first :( ", "OK");
                DanhSachChuyenDaDi = null;
            }
        }
        async Task GetChuadi(string url, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await client.GetAsync(url);

            System.Diagnostics.Debug.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                var st = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                var content = await response.Content.ReadAsStringAsync();
                DanhSachChuyenChuaDi = JsonConvert.DeserializeObject<List<Dieuxe>>(content,st);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("error", "you should be login first :( ", "OK");
                DanhSachChuyenChuaDi = null;
            }
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
