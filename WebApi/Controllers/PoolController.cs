using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoolController : ControllerBase
    {
        private readonly DbPoolDbContext _context;

        public PoolController(DbPoolDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/Pool/GetAllPools")]
        public IActionResult GetAllPools()
        {
            return Ok(_context.Pool.FromSql("Execute [Pool].[GetPools]").ToList());
        }

        public IEnumerable<PoolStatusProperties> GetPoolPropertiesById(int idPool, int idStatus)
        {
            return this._context.PoolStatusProperties.FromSql($"[Pool].[GetPoolPropertiesById] {idPool}, {idStatus}").ToList();
        }
        public Status GetStatusById(int idStatus)
        {
            return this._context.Status.FromSql($"[Pool].[GetStatusById] {idStatus}").FirstOrDefault();
        }
        public Property GetPropertyById(int idProperty)
        {
            return this._context.Property.FromSql($"[Pool].[GetPropertyById] {idProperty}").FirstOrDefault();
        }
        public DataType GetDataTypeById(int idDataType)
        {
            return this._context.DataType.FromSql($"[Pool].[GetDataTypeById] {idDataType}").FirstOrDefault();

        }
        public IEnumerable<Email> GetEmails()
        {
            return this._context.Email.FromSql("[Pool].[GetEmails]").ToList();
        }
        public void UpdatePoolSentEmail(int idPool)
        {
            this._context.Database.ExecuteSqlCommand($"[Pool].[UpdatePoolSentEmail] {idPool}");
        }

        // GET: api/Pool
        [HttpGet]
        public IEnumerable<Pool> GetPool()
        {
            //IEnumerable<Pool> pool = this._context.GetPools().OrderBy(x => x.idPool);
            //IEnumerable<Pool> pool = this._context.Pool.FromSql("Execute [Pool].[GetPools]").OrderBy(x => x.idPool);
            IEnumerable<Pool> pool = this.GetPools();

            foreach (var p in pool)
            {
                //p.PoolStatusP = this._context.GetPoolPropertiesById(p.idPool, p.idStatus);
                //p.PoolStatusP = (IEnumerable<PoolStatusProperties>)this._context.PoolStatusProperties.FromSql($"[Pool].[GetPoolPropertiesById] {p.idPool}, {p.idStatus}");
                p.PoolStatusP = GetPoolPropertiesById(p.idPool, p.idStatus);

                //p.StatusD = this._context.GetStatusById(p.idStatus);
                //p.StatusD = (Status)this._context.Pool.FromSql($"[Pool].[GetStatusById] {p.idStatus}");
                p.StatusD = this.GetStatusById(p.idStatus);

                p.StatusD.Animated = false;

                Alert res = this.AlertPool(p);  //wifron - debug: revisar que hace
                p.StatusD.Warning = false;
                if (res.Content)
                {
                    p.StatusD.Alert = res;
                    p.StatusD.Warning = true;
                }

                foreach (var s in p.PoolStatusP)
                {
                    //s.StatusD = this._context.GetStatusById(s.idStatus);
                    //s.StatusD = (Status)this._context.Pool.FromSql($"[Pool].[GetStatusById] {s.idStatus}");
                    s.StatusD = this.GetStatusById(s.idStatus);

                    //s.PropertyD = this._context.GetPropertyById(s.idProperty);
                    //s.PropertyD = (Property)this._context.Pool.FromSql($"[Pool].[GetPropertyById] {s.idProperty}");
                    s.PropertyD = this.GetPropertyById(s.idProperty);

                    if (s.PropertyD != null)
                    {
                        //s.PropertyD.DataTypeD = this._context.GetDataTypeById(s.PropertyD.idDataType);
                        //s.PropertyD.DataTypeD = (DataType)this._context.Pool.FromSql($"[Pool].[GetDataTypeById] {s.PropertyD.idDataType}");
                        s.PropertyD.DataTypeD = this.GetDataTypeById(s.PropertyD.idDataType);

                        if (s.PropertyD.Name == "STATUS_TIME")
                        {

                            string[] time = s.psValue.Split(':');

                            DateTimeOffset dto = s.ValueDateTime.GetValueOrDefault();

                            //DateTime dt = dto.DateTime;
                            DateTime dt = p.UpdDateTime.DateTime;

                            //DateTime dt1 = dto.DateTime;
                            DateTime dt1 = p.UpdDateTime.DateTime;

                            DateTime now = DateTime.Now;

                            TimeSpan result = now.Subtract(dt);

                            string st = time[0].ToString();

                            double sum = double.Parse(int.Parse(st).ToString());

                            dt = dt.AddHours(sum);

                            st = time[1].ToString();//

                            sum = double.Parse(int.Parse(st).ToString());
                            dt = dt.AddMinutes(sum);

                            TimeSpan limit = dt.Subtract(dt1);

                            double d1 = result.TotalSeconds;
                            double d2 = limit.TotalSeconds;

                            p.StatusD.Begin = Convert.ToInt64(d1);
                            p.StatusD.End = Convert.ToInt64(d2);

                            if (p.StatusD.Begin > p.StatusD.End)
                            {
                                //////
                                p.StatusD.BeginFinal = p.StatusD.Begin;
                                p.StatusD.EndFinal = p.StatusD.BeginFinal * 10000;

                                p.StatusD.CountDownFinal = true;

                                p.StatusD.Begin = p.StatusD.End;

                                if (!p.SentEmail)
                                {
                                    //Enviar el mail
                                    //IEnumerable<Email> emailList = this._context.GetEmails();
                                    //IEnumerable<Email> emailList = (IEnumerable<Email>)this._context.Pool.FromSql($"[Pool].[GetEmails]").ToList();
                                    IEnumerable<Email> emailList = this.GetEmails();

                                    string body = "Se ha cumplido el tiempo de la pileta " + p.Identification + " "
                                        + " en el estado " + p.StatusD.Name;

                                    foreach (var email in emailList)
                                    {

                                        // bool send = email.SendMail(email.EmailAddress, System.Configuration.ConfigurationManager.AppSettings["MAIL_USER"], body , "MIMICO PILETAS ESCORIA");
                                        bool send = false;
                                        if (send)
                                        {
                                            this.UpdatePoolSentEmail(p.idPool);
                                        }
                                    }

                                    /* Email sent = new Email();
                                     if (sent.SendMail("jlobos@trendingenieria.com.ar", "correo.prueba020202@gmail.com", "test","test"))
                                     {
                                         this._context.UpdatePoolSentEmail(p.idPool);
                                     }*/

                                }
                                p.StatusD.Animated = true;
                            }
                            p.StatusD.CountDown = true;
                        }
                    }
                }
            }
            return pool;
        }

        /*
        // GET: api/Pool/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPool([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pool = await _context.Pool.FindAsync(id);

            if (pool == null)
            {
                return NotFound();
            }

            return Ok(pool);
        }
        */

        /*
        // PUT: api/Pool/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPool([FromRoute] int id, [FromBody] Pool pool)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pool.idPool)
            {
                return BadRequest();
            }

            _context.Entry(pool).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoolExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        */

        /*
        // POST: api/Pool
        [HttpPost]
        public async Task<IActionResult> PostPool([FromBody] Pool pool)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Pool.Add(pool);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPool", new { id = pool.idPool }, pool);
        }
        */

        /*
        // DELETE: api/Pool/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePool([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pool = await _context.Pool.FindAsync(id);
            if (pool == null)
            {
                return NotFound();
            }

            _context.Pool.Remove(pool);
            await _context.SaveChangesAsync();

            return Ok(pool);
        }

        private bool PoolExists(int id)
        {
            return _context.Pool.Any(e => e.idPool == id);
        }
        */
        public IEnumerable<Pool> GetPools()
        {
            return this._context.Pool.FromSql("[Pool].[GetPools]").ToList();
            
        }

        public StatusEmailType GetEmailTypeStatus(int idStatus)
        {
            return this._context.StatusEmailType.FromSql($"[Pool].[GetEmailTypeStatus] {idStatus}").FirstOrDefault();
        }

        public Pool GetPoolByIdUnique(int idPool)
        {
            return this._context.Pool.FromSql($"[Pool].[GetPoolById] {idPool}").FirstOrDefault();
        }

        public EmailType GetEmailType(long idEmailType)
        {
            return this._context.EmailType.FromSql($"[Pool].[GetEmailType] {idEmailType}").FirstOrDefault();
        }

        private Alert AlertPool(Pool p)
        {
            Alert r = new Alert();
            r.Content = false;
            if (p.idPool != 6)
            {
                int statusIdP = p.idStatus;
                int poolIdP = p.idPool + 1;
                //StatusEmailType sEType = this._context.GetEmailTypeStatus(statusIdP);
                //StatusEmailType sEType = (StatusEmailType)this._context.Pool.FromSql($"[Pool].[GetEmailTypeStatus] {statusIdP}");
                StatusEmailType sEType = this.GetEmailTypeStatus(statusIdP);


                if (sEType != null)
                {
                    //var pPool = this._context.GetPoolByIdUnique(poolIdP);
                    //var pPool = (Pool)this._context.Pool.FromSql($"[Pool].[GetPoolById] {poolIdP}").FirstOrDefault();
                    var pPool = this.GetPoolByIdUnique(poolIdP);
                    if (pPool != null)
                    {
                        if (sEType.idStatusNextPool == pPool.idStatus)
                        {
                            //var emailType = _context.GetEmailType(sEType.idEmailType.Value);
                            //var emailType = (EmailType)this._context.Pool.FromSql($"[Pool].[GetEmailType] {sEType.idEmailType.Value}");
                            var emailType = GetEmailType(sEType.idEmailType.Value);
                            if (emailType != null && emailType.idEmailType != 0)
                            {
                                r.Title = emailType.EmailSubject;
                                string body = String.Format(emailType.EmailBody, p.Identification, pPool.Identification);
                                r.Message = r.Message + " " + body + ". ";
                                r.Content = true;
                            } //fin  if (emailType != null)
                        }
                    }
                }
            }

            if (p.idPool != 1)
            {
                int statusIdP = p.idStatus;
                int poolIdP = p.idPool - 1;

                //StatusEmailType sEType = this._context.GetEmailTypeStatus(statusIdP);
                //StatusEmailType sEType = (StatusEmailType)this._context.Pool.FromSql($"[Pool].[GetEmailTypeStatus] {statusIdP}");
                StatusEmailType sEType = this.GetEmailTypeStatus(statusIdP);

                if (sEType != null)
                {
                    //var pPool = this._context.GetPoolByIdUnique(poolIdP);
                    //var pPool = (Pool)this._context.Pool.FromSql($"[Pool].[GetPoolById] {poolIdP}").FirstOrDefault();
                    var pPool = this.GetPoolByIdUnique(poolIdP);
                    if (pPool != null)
                    {
                        if (sEType.idStatusNextPool == pPool.idStatus)
                        {
                            //var emailType = _context.GetEmailType(sEType.idEmailType.Value);
                            //var emailType = (EmailType)this._context.Pool.FromSql($"[Pool].[GetEmailType] {sEType.idEmailType.Value}");
                            var emailType = GetEmailType(sEType.idEmailType.Value);
                            if (emailType != null && emailType.idEmailType != 0)
                            {
                                r.Title = emailType.EmailSubject;
                                string body = String.Format(emailType.EmailBody, p.Identification, pPool.Identification);
                                r.Message = r.Message + " " + body + ". ";
                                r.Content = true;
                            } //fin  if (emailType != null)
                        }
                    }
                }
            }
            return r;
        }
    }
}