using System;
using System.Collections.Generic;

namespace DataJsonAnalytics.Models
{
    public class Segment
    {
        public DateTime segmentStart { get; set; }
        public DateTime segmentEnd { get; set; }
        public string queueId { get; set; }
        public string disconnectType { get; set; }
        public string segmentType { get; set; }
        public bool conference { get; set; }
    }

    public class Metric
    {
        public string name { get; set; }
        public int value { get; set; }
        public DateTime emitDate { get; set; }
    }

    public class Session
    {
        public string mediaType { get; set; }
        public string sessionId { get; set; }
        public string direction { get; set; }
        public List<Segment> segments { get; set; }
        public List<Metric> metrics { get; set; }
        public string provider { get; set; }
        public List<string> requestedRoutings { get; set; }
        public string usedRouting { get; set; }
        public string selectedAgentId { get; set; }
        public string peerId { get; set; }
        public string remote { get; set; }
    }

    public class Participant
    {
        public string participantId { get; set; }
        public string participantName { get; set; }
        public string purpose { get; set; }
        public List<Session> sessions { get; set; }
        public string userId { get; set; }
    }

    public class Conversation
    {
        public string conversationId { get; set; }
        public DateTime conversationStart { get; set; }
        public DateTime conversationEnd { get; set; }
        public double mediaStatsMinConversationMos { get; set; }
        public double mediaStatsMinConversationRFactor { get; set; }
        public string originatingDirection { get; set; }
        public List<string> divisionIds { get; set; }
        public List<Participant> participants { get; set; }
    }
}
