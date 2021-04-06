using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appiatech_Task.Models;
using Appiatech_Task.Repository;
using Appiatech_Task.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Appiatech_Task.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
            public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        // GET: api/<PersonController>
        [HttpGet]
        public ResourceCollection<Person> Get(string filter, int? pageNumber, int? pageSize)
        {
            var personCount = _personRepository.Count(null);
            var items = new List<Person>();
            if (filter != null)
            {
                items = _personRepository.GetAllPerson(pageNumber, pageSize, x =>
                                                             x.Name.ToLower().Contains(filter.ToLower()) ||
                                                             x.Email.ToLower().Contains(filter.ToLower()) ||
                                                             x.Phone.Contains(filter.ToLower())).ToList();
            }
            else
            {
                items = _personRepository.GetAllPerson(pageNumber, pageSize).ToList();
            }
            var collection = new ResourceCollection<Person>(items, personCount);
            return collection;
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Person person = await _personRepository.GetById(id);
            if(person == null)
            {
                throw new Exception($"The person with Id : {id} not found.");
            }
            else
            {
                return Ok(person);
            }
        }

        // POST api/<PersonController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonViewModel model)
        {
            var count = _personRepository.Count(x => x.Email.ToLower() == model.Email.ToLower());
            if(count > 0)
            {
                throw new Exception($"The person {model.Name} already exists.");
            }
            Person person = new Person();
            person.Name = model.Name;
            person.Email = model.Email;
            person.Phone = model.Phone;
            person.CreatedOn = DateTime.Now;
            await _personRepository.Create(person);

            return Created(Url.Link("", new { id = person.Id }), person);
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PersonViewModel model)
        {
            Person person = await _personRepository.GetById(id);
            var count = _personRepository.Count(x => x.Email.ToLower() != person.Email.ToLower() &&
                                                     x.Email.ToLower() == model.Email.ToLower());
            if(count > 0)
            {
                throw new Exception("Something is exists.");
            }
            person.Name = model.Name;
            person.Email = model.Email;
            person.Phone = model.Phone;
            person.ModifiedOn = DateTime.Now;
            await _personRepository.Update(person);

            return Ok(person);
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Person person = await _personRepository.GetById(id);
            await _personRepository.Remove(person);
            return NoContent();
        }
    }
}
