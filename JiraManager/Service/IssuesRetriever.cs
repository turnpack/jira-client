﻿using JiraManager.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiraManager.Service
{
   public class IssuesRetriever
   {
      private readonly Configuration _configuration;

      public IssuesRetriever(Configuration configuration)
      {
         _configuration = configuration;
      }

      public async Task<IEnumerable<RawIssue>> RetrieveIssues(string jql)
      {
         var results = new List<RawIssue>();

         

         return results;
      }
   }
}