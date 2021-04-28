using GardenPlannerData;
using GardenPlannerModels.AnswerModels;
using GardenPlannerModels.CommentModels;
using GardenPlannerModels.LikesModel;
using GardenPlannerModels.QuestionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GardenPlannerServices
{
    
    public class SocialInteractionsService
    {
        private readonly ApplicationDbContext ctx = new ApplicationDbContext();
        private readonly Guid _userID;
        public SocialInteractionsService(Guid userID)
        {
            _userID = userID;
        }

        //AddComment Method allows to post a comment on a plant by taking information like plantID , Title and comment.
        public bool AddComment(AddCommentModel model)
        {
            Comments comments = new Comments
            {
                PlantID = model.PlantID,
                Title = model.Title,
                Comment = model.Comment,
                UserID = _userID,
                CreatedDate = DateTimeOffset.UtcNow
            };
            ctx.Comments.Add(comments);
            return ctx.SaveChanges() == 1;
        }

        //AlterLikePlant allows to like a plant with given plant ID.
        public bool AlterLikePlant(AlterLikeModel model)
        {
            if (ctx.Likes.Where(e => e.PlantID == model.PlantID && e.UserID == _userID).Count() < 1) 
            {
                Likes likes = new Likes
                {
                    IsLiked = true,
                    PlantID = model.PlantID,
                    UserID = _userID,
                    CreatedDate = DateTimeOffset.UtcNow,
                };
                ctx.Likes.Add(likes);
            }
            else
            {
                bool isLiked = ctx.Likes.Single(e => e.PlantID == model.PlantID && e.UserID == _userID).IsLiked;
                if (isLiked)
                {
                    ctx.Likes.Single(e => e.PlantID == model.PlantID && e.UserID == _userID).IsLiked = false;
                    ctx.Likes.Single(e => e.PlantID == model.PlantID && e.UserID == _userID).ModifiedDate = DateTimeOffset.UtcNow;
                }
                else
                {
                    ctx.Likes.Single(e => e.PlantID == model.PlantID && e.UserID == _userID).IsLiked = true;
                    ctx.Likes.Single(e => e.PlantID == model.PlantID && e.UserID == _userID).ModifiedDate = DateTimeOffset.UtcNow;
                }
            }
            return ctx.SaveChanges() == 1;
        }

        //Add questions method  allows to post questions on plant by taking PlantID and Question.
       
        public bool AddQuestion(AddQuestionModel model)
        {
            Questions questions = new Questions
            {
                PlantID = model.PlantID,
                Question = model.Question,
                UserID = _userID,
                CreatedDate = DateTimeOffset.UtcNow
            };
            ctx.Questions.Add(questions);
            return ctx.SaveChanges() == 1;
        }

       //AddAnswer Method uses AddAnswerModel and allows to post answer for the question posted on plant with giving QuestionID.
        public bool AddAnswer(AddAnswerModel model)
        {
            Answers answers = new Answers
            {
                QuestionID = model.QuestionID,
                Answer = model.Answer,
                UserID = _userID,
                CreatedDate = DateTimeOffset.UtcNow
            };
            ctx.Answers.Add(answers);
            return ctx.SaveChanges() == 1;
        }

        //GetComments returns all the comments on a plant which matches the given plantID, with Palnt name and comments.
        public GetCommentsModel GetComments(int plantID) 
        {
            Plants plants = ctx.Plants.Single(e => e.PlantID == plantID);
            GetCommentsModel query =  new GetCommentsModel
            {
                PlantID = plants.PlantID,
                PlantName = plants.Name,
                Comments = ctx.Comments.Where(e => e.PlantID == plants.PlantID).Select(g => new CommentModel
                {
                    Title = g.Title,
                    Comment = g.Comment,
                    Username = ctx.Users.FirstOrDefault(e => e.Id.ToString() == g.UserID.ToString()).UserName,
                    CreatedDate = g.CreatedDate
                }).ToList(),
                Likes = ctx.Likes.Where(e => e.PlantID == plants.PlantID).Select(g => g.IsLiked == true).Count()
            };
            return query;
        }

        //GetLikes method returns likes of a plant which matches with the given plantID.
        public int GetLikes(int plantID)
        {
            int count = 0;
            List<Likes> likes = ctx.Likes.Where(e => e.PlantID == plantID).ToList();
            foreach (Likes item in likes)
            {
                if (item.IsLiked)
                {
                    count++;
                }
            }
            return count;
        }

        //GetQuestions method takes PlantID and return all the questions posted on the plant that matches given plantID.
        public IEnumerable<GetQuestionsModel> GetQuestions(int plantID)
        {
            var query = ctx.Questions.Where(e => e.PlantID == plantID).Select(f => new GetQuestionsModel
            {
                QuestionID = f.QuestionID,
                Question = f.Question,
                CreatedDate = f.CreatedDate
            });
            return query.ToList();
        }

        //Getquestions method takes Questions and and returns the question matches to questionID and all the answers posted on the question.
        public GetQuestionAnswerModel GetQuestionAnswers(int questionID) 
        {
            Questions questions = ctx.Questions.Single(e => e.QuestionID == questionID);
            GetQuestionAnswerModel getQuestionAnswerModel = new GetQuestionAnswerModel
            {
                Question = questions.Question,
                CreatedDate = questions.CreatedDate,
                Answers = ctx.Answers.Where(e => e.QuestionID == questions.QuestionID).Select(g => new AnswerModel
                {
                    Answer = g.Answer,
                    CreatedDate = g.CreatedDate,
                    Username = ctx.Users.FirstOrDefault(e => e.Id.ToString() == g.UserID.ToString()).UserName
                }).ToList(),
            };
            return getQuestionAnswerModel;
        }
    }
}
