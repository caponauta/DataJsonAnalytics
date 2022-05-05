using DataJsonAnalytics.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Net;
using System.Net.Http;

namespace DataJsonAnalytics.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MainController : ControllerBase
    {
        [Route("save-data")]
        [HttpPost]
        public HttpResponseMessage SaveData(Conversation conversation)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

            try
            {
                if (conversation != null)
                {
                    // Guardar registros
                    // conversation
                    string QueryConversation = "insert into conversations values ('" + conversation.conversationId + "', '" + conversation.conversationStart.ToString("yyyy-MM-ddTHH:mm:ss.FFFZ") + "' ,'" + conversation.conversationEnd.ToString("yyyy-MM-ddTHH:mm:ss.FFFZ") + "'," + conversation.mediaStatsMinConversationMos.ToString().Replace(",", ".") + "," + conversation.mediaStatsMinConversationRFactor.ToString().Replace(",", ".") + ",'" + conversation.originatingDirection + "','" + conversation.divisionIds[0] + "')";
                    SaveDataAdo(QueryConversation);

                    if (conversation.participants.Count > 0)
                    {
                        foreach (var itemParticipant in conversation.participants)
                        {
                            string QueryParticipants = "insert into participants values ('" + itemParticipant.participantId + "', '" + conversation.conversationId + "','" + itemParticipant.participantName + "','" + itemParticipant.purpose + "','" + itemParticipant.userId + "');";
                            SaveDataAdo(QueryParticipants);

                            if (itemParticipant.sessions.Count > 0)
                            {
                                foreach (var itemSession in itemParticipant.sessions)
                                {
                                    string QuerySession = "insert into sessions values ('" + itemSession.sessionId + "', '" + itemParticipant.participantId + "','" + itemSession.mediaType + "','" + itemSession.direction + "', '" + itemSession.peerId + "', '" + itemSession.provider + "', '" + itemSession.requestedRoutings[0] + "','" + itemSession.selectedAgentId + "','" + itemSession.remote + "');";
                                    SaveDataAdo(QuerySession);

                                    if (itemSession.segments.Count > 0)
                                    {
                                        foreach (var itemSegment in itemSession.segments)
                                        {
                                            // segment_id is session_id + _ + segment_start + _ + segment_end
                                            string segment_id = itemSession.sessionId + "_" + itemSegment.segmentStart + "_" + itemSegment.segmentEnd;

                                            //Insert segmento
                                            string QuerySegment = "insert into segments values ('" + segment_id + "','" + itemSession.sessionId + "','" + itemSegment.segmentStart.ToString("yyyy-MM-ddTHH:mm:ss.FFFZ") + "','" + itemSegment.segmentEnd.ToString("yyyy-MM-ddTHH:mm:ss.FFFZ") + "','" + itemSegment.segmentType + "','" + itemSegment.conference + "','" + itemSegment.disconnectType + "','" + itemSegment.queueId + "');";
                                            SaveDataAdo(QuerySegment);
                                        }
                                    }

                                    if (itemSession.metrics.Count > 0)
                                    {
                                        foreach (var itemMetric in itemSession.metrics)
                                        {
                                            // metric_id is session_id + _ + name + _ + value
                                            string metric_id = itemSession.sessionId + "_" + itemMetric.name + "_" + itemMetric.value;

                                            //Insert metricas
                                            string QueryMetrics = "insert into metrics values ('" + metric_id + "','" + itemSession.sessionId + "','" + itemMetric.name + "'," + itemMetric.value + ",'" + itemMetric.emitDate.ToString("yyyy-MM-ddTHH:mm:ss.FFFZ") + "')";
                                            SaveDataAdo(QueryMetrics);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    httpResponseMessage.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    httpResponseMessage.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (System.Exception)
            {
                httpResponseMessage.StatusCode = HttpStatusCode.InternalServerError;
            }

            return httpResponseMessage;
        }

        public void SaveDataAdo(string Command)
        {
            try
            {
                var connection = "Host=localhost;Username=postgres;Password=asd123;Database=JsonDataExample";

                NpgsqlConnection con = new NpgsqlConnection(connection);
                con.Open();

                var cmd = new NpgsqlCommand();
                cmd.Connection = con;
                //cmd.CommandText = "insert into conversations values ('" + conversation.conversationId + "', '" + conversation.conversationStart.ToString("yyyy-MM-ddTHH:mm:ss.F") + "' ,'" + conversation.conversationEnd.ToString("yyyy-MM-ddTHH:mm:ss.F") + "'," + conversation.mediaStatsMinConversationMos + "," + conversation.mediaStatsMinConversationRFactor + ",'" + conversation.originatingDirection + "','" + conversation.divisionIds[0] + "')";
                cmd.CommandText = Command;
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
