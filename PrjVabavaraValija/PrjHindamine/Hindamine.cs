using PrjAndmebaas;

namespace PrjHindamine
{
    public class Hindamine : IHindamine
    {
        List<Skoorid> IHindamine.HindaTarkvarad(List<Tarkvaranõuded> sobivadTarkvarad, List<Kriteeriumid> valitudKriteeriumid, ILoeAndmed andmed)
        {
            List<Skoorid> tulemused = new List<Skoorid>();

            foreach (Tarkvaranõuded tarkvara in sobivadTarkvarad)
            {
                List<int> tarkvaraKriteeriumid = andmed.LoeTarkvaraKriteeriumid(tarkvara.Id);

                List<Kriteeriumid> sobivadKriteeriumid = new List<Kriteeriumid>();

                List<Kriteeriumid> puuduvadKriteeriumid = new List<Kriteeriumid>();

                foreach(Kriteeriumid valitud in valitudKriteeriumid)
                {
                    if(tarkvaraKriteeriumid.Contains(valitud.Id))
                    {
                        sobivadKriteeriumid.Add(valitud);
                    }
                    else
                    {
                        puuduvadKriteeriumid.Add(valitud);
                    }
                }

                tulemused.Add(new Skoorid
                {
                    TarkvaraId = tarkvara.Id,
                    Nimi = tarkvara.Nimi,
                    Skoor = sobivadKriteeriumid.Count,
                    MaxSkoor = valitudKriteeriumid.Count,
                    SobivadKriteeriumid = sobivadKriteeriumid,
                    PuuduvadKriteeriumid = puuduvadKriteeriumid

                });
            }

            return tulemused.OrderByDescending(t => t.Skoor).ToList();
        }
    }
}
