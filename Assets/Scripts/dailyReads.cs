using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class dailyReads  {


	public static string todayRead()
    {
        string date = System.DateTime.Now.ToString("yyyy/MM/dd");
        string lastDate = PlayerPrefs.GetString("lastDate");
        int lastIndex = PlayerPrefs.GetInt("lastIndex");
        
        if(date == lastDate)
        {
            return reads.texts[lastIndex];
        }

        ++lastIndex;
        if (reads.texts.Length >= lastIndex)
            lastIndex = 0;

        PlayerPrefs.SetString("lastDate", date);
        PlayerPrefs.SetInt("lastIndex", lastIndex);

        return reads.texts[lastIndex];
    }
}

//hardcoded daily reads :(
public class reads
{
    //random story
    public static string[] texts = {   "A fost odată ca niciodată un tăietor de lemne tare nevoiaş şi omul ăsta îşi avea căscioara la marginea unui codru nesfârşit, unde-şi ducea viaţa împreună cu nevastă-sa şi cei doi copii ai săi. Şi pe băieţel îl chema Hansel, iar pe fetiţă Gretel. Seara în pat, pe bietul om începeau să-l muncească gândurile şi, zvârcolindu-se neliniştit în aşternut, se pomenea că oftează cu grea obidă. Şi-ntr-una din aceste seri îi zise el neveste-sii: – Ce-o să ne facem, femeie? Cu ce-o să-i hrănim pe bieţii noştri copii, când nici pentru noi nu mai avem nici de unele? – Ştii ceva, bărbate, răspunse femeia, mâine-n zori luăm copiii cu noi şi-i ducem unde-i pădurea mai deasă. Le facem un foc bun, le dăm şi câte-o îmbucătură de pâine şi pe urmă ne vedem de treburile noastre. Iar pe ei îi lăsăm acolo. De nimerit, n-or să mai nimerească drumul spre casă, de asta sunt sigură, şi-n felul ăsta ne descotorosim de ei! – Nu, femeie, asta n-o s-o fac nici în ruptul capului, spuse bărbatul. Nu mă rabdă inima să-mi las copiii singuri în pădure. Că doar multă vreme n-ar trece şi-ar veni fiarele să-i sfâşie, – Vai de tine, neghiobule, îl luă femeia la rost, de-i aşa, o să murim de foame toţi patru… Poţi să ciopleşti de pe-acum scânduri pentru sicrie…" ,
                                "A fost odată o fetiţă zglobie şi drăgălaşă, pe care o iubea oricine de cum o vedea. Dar mai dragă decât oricui îi era ea bunicii, care nu ştia ce daruri să-i mai facă. Odată, bunica îi dărui o scufiţă de catifea roşie şi pentru că-i şedea tare bine fetiţei şi nici nu mai voia să poarte altceva pe cap, o numiră de atunci Scufiţa Roşie. Într-o zi, maică-sa îi zise:   – Scufiţă Roşie, ia bagă-n coşuleţ bucata asta de cozonac şi sticla asta cu vin şi du-le bunicii, că e bolnavă şi slăbită şi bunătăţile astea o să-i ajute să-şi mai vină în puteri. Da’ vezi de pleacă mai înainte de-a se lăsa zăpuşeala şi caută de mergi frumos şi să nu te abaţi din drum; altfel, cine ştie, de alergi, ai putea să cazi şi să spargi sticla şi atunci bunicuţa cu ce o să se mai aleagă? Iar când o fi să intri în casă, nu uita să-i dai bunicii \"bună dimineaţa\" şi vezi să nu înceapă a-ţi umbla ochii prin toate ungherele!",
                                "A fost odată un om şi omul ăsta avea un măgar, care de ani şi ani tot cărase la moară saci cu grăunţe. Dar de la un timp bietului dobitoc i se împuţinaseră puterile şi nu mai era bun de nici o treabă. De aceea, stăpânul lui se gândi într-una din zile că n-ar mai avea nici un rost să strice pe el bunătate de nutreţ. Măgarul pricepu însă că nu-l aşteaptă vremuri prea bune şi, fără să mai adaste, îşi luă tălpăşiţa spre oraşul Bremen. Nu ştiu de unde-i venise-n gând că acolo s-ar putea face muzicant al oraşului. După ce merse el o bucată de vreme, iată că dădu peste un ogar care zăcea întins pe-o margine a drumului, răsuflând din greu de parcă ar fi făcut ocolul pământului. – Ce gâfâi aşa, mă Apucă-l-în-Colţi? îl întrebă măgarul. – Vai de păcatele mele, răspunse câinele, pentru că sunt bătrân şi slăbesc din zi în zi tot mai mult şi pentru că la vânătoare nu mă mai dovedesc bun de nici o ispravă, stăpânul meu şi-a pus în gând să-mi facă de petrecanie, şi atunci mi-am luat repede tălpăşiţa. Dar vorba e cu ce-o să-mi câştig eu pâinea de-aci înainte?"};
}
