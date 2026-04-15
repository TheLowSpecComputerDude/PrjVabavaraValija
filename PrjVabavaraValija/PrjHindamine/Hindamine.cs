using PrjAndmebaas;

namespace PrjHindamine
{
    public class Hindamine : IHindamine
    {
        public List<Skoorid> HindaTarkvarad(List<Tarkvaranõuded> sobivadTarkvarad, List<int> valitudKriteeriumid, ILoeAndmed andmed)
        {
            List<Skoorid> tulemused = new List<Skoorid>();

            foreach (Tarkvaranõuded tarkvara in sobivadTarkvarad)
            {
                List<int> tarkvaraKriteeriumid = andmed.LoeTarkvaraKriteeriumid(tarkvara.Id);

                int skoor = 0;

                foreach(int valitudId in valitudKriteeriumid)
                {
                    if(tarkvaraKriteeriumid.Contains(valitudId))
                    {
                        skoor++;
                    }
                }

                tulemused.Add(new Skoorid
                {
                    TarkvaraId = tarkvara.Id,
                    Nimi = tarkvara.Nimi,
                    Skoor = skoor,
                    MaxSkoor = valitudKriteeriumid.Count

                });
            }

            return tulemused.OrderByDescending(t => t.Skoor).ToList();
        }
    }
}
