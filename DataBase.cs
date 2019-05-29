using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_FlightDB
{
    class DataBase
    {
        /*Создаем обобщенный список Багажей. В этом списке будут храниться только объекты типа BaggageOwnerInfo не более. Этот список будет служить некой базой
         данных*/
        List<BaggageOwnerInfo> db;

        /*Создаем конструктор нашего класса DataBase который при создании будет инициализировать новый объект нашего списка BaggageDB, по факту создавая область
         в памяти нашего списка в котором на момент создания будет 0 элементов*/
        public DataBase()
        {
            db = new List<BaggageOwnerInfo>();
        }
        /*public - ключевое слово которое делает поле досутпным везде. Данное свойство которое возвращает нам наш список*/
        public List<BaggageOwnerInfo> Db { get => db;}
    }
}
