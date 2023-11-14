using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AddressBook.Controllers
{
    public class AddressBookAPIController : ApiController
    {
        AddressBookDB db = new AddressBookDB();

        // GET: api/AddressBookAPI
        public IEnumerable<Person> Get()
        {
            return db.People.OrderBy(x => x.Type).ThenBy(x => x.Dep).ThenBy(x => x.Name_CH).ToList();
        }

        // GET: api/AddressBookAPI/5
        public IEnumerable<Person> Get(string id)
        {
            return db.People.Where(x => x.Name_CH.Contains(id) || x.Name_EN.Contains(id) || x.UserNo.Contains(id) || x.Extension_Num.Contains(id) || x.Dep.Contains(id))
                    .OrderBy(x => x.Type).ThenBy(x => x.Dep).ThenBy(x => x.Name_CH).ToList();
        }

        // POST: api/AddressBookAPI
        public void Post([FromBody]Person person)
        {
            db.People.Add(person);
            db.SaveChanges();
        }

        // PUT: api/AddressBookAPI/5
        public void Put([FromBody] Person person)
        {
            Person new_person = db.People.Where(x => x.Id.Equals(person.Id)).First();
            new_person.Name_CH = person.Name_CH;
            new_person.Name_EN = person.Name_EN;
            new_person.UserNo = person.UserNo;
            new_person.Extension_Num = person.Extension_Num;
            new_person.Dep = person.Dep;
            new_person.Type = person.Type;

            db.Entry(new_person).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        // DELETE: api/AddressBookAPI/5
        public void Delete([FromBody] Person person)
        {
            Person new_person = db.People.Where(x => x.Id.Equals(person.Id)).First();
            db.People.Remove(new_person);
            db.SaveChanges();
        }
    }
}
