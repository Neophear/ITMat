﻿using ITMat.Core.DTO;
using ITMat.UI.WindowsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.UI.WindowsApp.Services.MockService
{
    public class MockCommentService : AbstractMockService, ICommentService
    {
        private readonly IDictionary<int, List<CommentDTO>> employeeComments;
        private int counter = 0;

        public MockCommentService()
        {
            employeeComments = new Dictionary<int, List<CommentDTO>>();

            for (int i = 1; i <= 2; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var text = j % 3 == 0 ? $"Autogenerated long comment that is very long and takes up a lot of precious space, which is really annoying to design around." : $"Autogenerated comment";
                    AddEmployeeComment(i, Environment.UserName, text, DateTime.Now.AddDays(0 - j));
                }
            }
        }

        private int AddEmployeeComment(int employeeId, string username, string text, DateTime? createdTime = null)
        {
            counter++;

            if (!employeeComments.TryGetValue(employeeId, out List<CommentDTO> comments))
                comments = employeeComments[employeeId] = new List<CommentDTO>();

            comments.Add(new CommentDTO
            {
                Id = counter,
                CreatedTime = createdTime ?? DateTime.Now,
                Username = username,
                Text = text
            });

            return counter;
        }

        public async Task<int> CreateEmployeeCommentAsync(int employeeId, string text)
            => await ExecuteWithDelay(() => AddEmployeeComment(employeeId, Environment.UserName, text));

        public async Task<int> CreateLoanCommentAsync(int loanId, string text)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteCommentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CommentDTO>> GetEmployeeCommentsAsync(int employeeId)
            => await ExecuteWithDelay(() => employeeComments.TryGetValue(employeeId, out List<CommentDTO> result) ? result.ToArray() : new CommentDTO[0]);

        public async Task<IEnumerable<CommentDTO>> GetLoanCommentsAsync(int loanId)
        {
            throw new NotImplementedException();
        }
    }
}