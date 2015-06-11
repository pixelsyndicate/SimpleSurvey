using System.Collections.Generic;
using System.Linq;
using ssWeb.Repositories;

namespace ssWeb.Models
{
    public class SimpleSurveyManager : ISimpleSurveyManager
    {
        private ISurveyRepository _surveyRepo;
        private IQuestionRepository _questionRepo;
        private ISurveyResponsesRepository _responseRepo;

        private SimpleSurveyManager(ISurveyRepository surveyRepo)
        {
            _surveyRepo = surveyRepo;
        }

        private SimpleSurveyManager(IQuestionRepository questionRepo)
        {
            _questionRepo = questionRepo;
        }

        private SimpleSurveyManager(ISurveyResponsesRepository responseRepo)
        {
            _responseRepo = responseRepo;
        }

        public User GetNewUserFromIdentity()
        {
            User newUser = new User();

            return newUser;
            ;
        }

        private void PopulateSurvey()
        {
            //    List<Question> questions = (from p in context.Questions
            //                                join q in context.SurveyQuestions on p.ID equals q.QuestionID
            //                                where q.SurveyID == surveyid
            //                                select p).ToList();
            //    Table tbl = new Table();
            //    tbl.Width = Unit.Percentage(100);
            //    TableRow tr;
            //    TableCell tc;
            //    TextBox txt;
            //    CheckBox cbk;
            //    DropDownList ddl;
            //    foreach (Question q in questions)
            //    {
            //        tr = new TableRow();
            //        tc = new TableCell();
            //        tc.Width = Unit.Percentage(25);
            //        tc.Text = q.Text;
            //        tc.Attributes.Add("id", q.ID.ToString());
            //        tr.Cells.Add(tc);
            //        tc = new TableCell();
            //        if (q.QuestionType.ToLower() == "singlelinetextbox")
            //        {
            //            txt = new TextBox();
            //            txt.ID = "txt_" + q.ID;
            //            txt.Width = Unit.Percentage(40);
            //            tc.Controls.Add(txt);
            //        }
            //        if (q.QuestionType.ToLower() == "multilinetextbox")
            //        {
            //            txt = new TextBox();
            //            txt.ID = "txt_" + q.ID;
            //            txt.TextMode = TextBoxMode.MultiLine;
            //            txt.Width = Unit.Percentage(40);
            //            tc.Controls.Add(txt);
            //        }
            //        if (q.QuestionType.ToLower() == "singleselect")
            //        {
            //            ddl = new DropDownList();
            //            ddl.ID = "ddl_" + q.ID;
            //            ddl.Width = Unit.Percentage(41);
            //            if (!string.IsNullOrEmpty(q.Options))
            //            {
            //                string[] values = q.Options.Split(',');
            //                foreach (string v in values)
            //                    ddl.Items.Add(v.Trim());
            //            }
            //            tc.Controls.Add(ddl);
            //        }
            //        tc.Width = Unit.Percentage(80);
            //        tr.Cells.Add(tc);
            //        tbl.Rows.Add(tr);
            //    }
            //    pnlSurvey.Controls.Add(tbl);
        }


        private List<SurveyResponse> GetSurveyReponse()
        {
            List<SurveyResponse> response = new List<SurveyResponse>();

            //foreach (Control ctr in pnlSurvey.Controls)
            //{
            //    if (ctr is Table)
            //    {
            //        Table tbl = ctr as Table;
            //        foreach (TableRow tr in tbl.Rows)
            //        {
            //            SurveyResponse sres = new SurveyResponse();
            //            sres.FilledBy = 2;
            //            sres.SurveyID = surveyid;
            //            sres.QuestionID = Convert.ToInt32(tr.Cells[0].Attributes["ID"]);
            //            TableCell tc = tr.Cells[1];
            //            foreach (Control ctrc in tc.Controls)
            //            {
            //                if (ctrc is TextBox)
            //                {
            //                    sres.Response = (ctrc as TextBox).Text.Trim();
            //                }
            //                else if (ctrc is DropDownList)
            //                {
            //                    sres.Response = (ctrc as DropDownList).SelectedValue;
            //                }
            //                else if (ctrc is CheckBox)
            //                {
            //                    sres.Response = (ctrc as CheckBox).Checked.ToString();
            //                }
            //            }
            //            response.Add(sres);
            //        }
            //    }
            //}

            return response;
        }


        public SurveyQuestionAnswerViewModel GetSurveyViewModelBySurveyId(int id)
        {

            var rawSurvey = _surveyRepo.Get(id);
            SurveyQuestionAnswerViewModel toReturnSingle;
            using (simpleSurvey1Entities db = new simpleSurvey1Entities())
            {
                var surveyModel = from sq in db.Survey_Questions
                    // join table surveys to questions
                    join survey in db.Surveys on sq.SurveyID equals survey.ID // link to Surveys
                    join surveyuser in db.Users on survey.CreatedBy equals surveyuser.ID // Survey Created By
                    join question in db.Questions on sq.QuestionID equals question.ID
                    // link to Questions
                    join surveyresponse in db.SurveyResponses on survey.ID equals surveyresponse.SurveyID
                    // responses of Survey
                    join responsefilledby in db.Users on surveyresponse.FilledBy equals responsefilledby.ID
                    // Response Filled By
                    where survey == rawSurvey
                    select new SurveyQuestionAnswerViewModel
                    {
                        Survey = survey,
                        Response = surveyresponse,
                        SurveyCreatedBy = surveyuser,
                        ResponseBy = responsefilledby,
                        UserTakingSurveyRole = surveyuser.Role1
                    };

                toReturnSingle = surveyModel.FirstOrDefault();
            }
            return toReturnSingle;
        }

        public bool? IsActive(int id)
        {
            var getModels = GetSurveyQuestionAnswerViewModels();
            var lastOne = getModels.LastOrDefault(i => i.Survey.ID == id);

            return (lastOne == null) ? (bool?)null : lastOne.Survey.Publish;
        }

        /// <summary>
        /// This is used to get a complete model of the Surveys, Responses, and Who created the survey and responsed to the survey questions
        /// </summary>
        /// <returns></returns>
        public IList<SurveyQuestionAnswerViewModel> GetSurveyQuestionAnswerViewModels()
        {

            List<SurveyQuestionAnswerViewModel> toReturnCollection;
            using (simpleSurvey1Entities db = new simpleSurvey1Entities())
            {

                var surveyModel = from sq in db.Survey_Questions
                    // join table surveys to questions
                    join survey in db.Surveys on sq.SurveyID equals survey.ID // link to Surveys
                    join surveyuser in db.Users on survey.CreatedBy equals surveyuser.ID // Survey Created By
                    join question in db.Questions on sq.QuestionID equals question.ID
                    // link to Questions
                    join surveyresponse in db.SurveyResponses on survey.ID equals surveyresponse.SurveyID
                    // responses of Survey
                    join responsefilledby in db.Users on surveyresponse.FilledBy equals responsefilledby.ID
                    // Response Filled By
                    select new SurveyQuestionAnswerViewModel
                    {
                        Survey = survey,
                        Response = surveyresponse,
                        SurveyCreatedBy = surveyuser,
                        ResponseBy = responsefilledby,
                        UserTakingSurveyRole = surveyuser.Role1
                    };

                toReturnCollection = surveyModel.ToList();
            }

            return toReturnCollection;
        }

        public static ISimpleSurveyManager GetQuestionManager()
        {
            return new SimpleSurveyManager(new QuestionRepository());
        }

        public static ISimpleSurveyManager GetSurveymanager()
        {
            return new SimpleSurveyManager(new SurveyRepository());
        }

        public static ISimpleSurveyManager GetSurveyResponseManager()
        {
            return new SimpleSurveyManager(new SurveyResponsesRepository());
        }
    }
}