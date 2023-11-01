using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoFramework.DataAccess
{
    internal class SessionDao
    {
        /// <summary>
        /// Create a new Session in the Database, and return the SessionId to be used for Time Trials
        /// </summary>
        /// <returns></returns>
        public int NewSession(string sessionName = "")
        {
            var sessionId = 0;
            var currentSessions = new List<Session>();  

            using (var db = new GranTurismoDb())
            {
                var session = new Session()
                {
                    Date = DateTime.Now,
                    Name = sessionName,
                };

                db.Sessions.Add(session);
                db.SaveChanges();

                currentSessions = db.Sessions.ToList();
            }

            var recentSession = currentSessions.Last();
            sessionId = recentSession?.SessionId ?? 0;

            return sessionId;
        }
    }
}
