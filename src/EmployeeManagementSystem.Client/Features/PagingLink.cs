﻿namespace EmployeeManagementSystem.Client.Features
{
    public class PagingLink
    {
        public string Text { get; set; }
        public int Page { get; set; }
        public bool Enabled { get; set; }
        public bool Active { get; set; }
        public PagingLink(int page, bool enabled, string text)
        {
            Text = text;
            Enabled = enabled;
            Page = page;
        }
    }
}
