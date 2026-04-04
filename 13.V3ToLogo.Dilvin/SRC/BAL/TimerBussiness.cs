using System;
using System.Collections.Generic;
using V3ToLogo.MODEL.GENERAL;
using System.Linq;

namespace V3ToLogo.BAL
{
    public static class TimerBussiness
    {
        public static void PrepareTimeList(out string errMsg)
        {
            errMsg= GeneralBussines.SUCCESS_MSG;
            try
            {
                if (GeneralBussines.OperationTimeList == null)
                    GeneralBussines.OperationTimeList = new List<TimeList>();
                else
                    GeneralBussines.OperationTimeList.Clear();
                errMsg = string.Empty;
                List<MODEL.ACCESSDB.Timer> timerMainList = GetTimerMainList(out errMsg);
                foreach (var timerMain in timerMainList)
                {
                    DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
                    int checkID = timerMain.ID;
                    int checkType = timerMain.Type_;
                    DateTime checkTime = timerMain.StartTime;
                    switch (checkType)
                    {
                        case 0:
                            {
                                if (currentDate <= checkTime)
                                {
                                    GeneralBussines.OperationTimeList.Add(new TimeList() { CheckTime = checkTime, Used = false });
                                }
                            } break;
                        case 1:
                            {
                                for (int j = 1; j < 366; j++)
                                {
                                    DateTime myDate = currentDate.AddDays(j);
                                    if (currentDate <= myDate)
                                    {
                                        TimeList timeItem = new TimeList();
                                        timeItem.CheckTime = new DateTime(DateTime.Now.Year, myDate.Month, myDate.Day, checkTime.Hour, checkTime.Minute, 0);
                                        timeItem.Used = false;
                                        GeneralBussines.OperationTimeList.Add(timeItem);
                                    }
                                }
                            } break;
                        case 2:
                            {
                                List<MODEL.ACCESSDB.TimerDetail> timerDetailList = new List<MODEL.ACCESSDB.TimerDetail>();
                                timerDetailList = GetTimerDetailList(checkID, out errMsg);
                                List<int> dayList = new List<int>();
                                foreach (var timerDtlItem in timerDetailList)
                                {
                                    dayList.Add(Convert.ToInt32(timerDtlItem.Value2.ToString()));
                                }

                                for (int j = 1; j < 366; j++)
                                {
                                    DateTime myDate = currentDate.AddDays(j);
                                    if (currentDate <= myDate && dayList.Contains((int)myDate.DayOfWeek))
                                    {
                                        TimeList timeItem = new TimeList();
                                        timeItem.CheckTime = new DateTime(DateTime.Now.Year, myDate.Month, myDate.Day, checkTime.Hour, checkTime.Minute, 0);
                                        timeItem.Used = false;
                                        GeneralBussines.OperationTimeList.Add(timeItem);
                                    }
                                }
                            } break;
                        case 3:
                            {
                                List<MODEL.ACCESSDB.TimerDetail> timerDetailList = new List<MODEL.ACCESSDB.TimerDetail>();
                                timerDetailList = GetTimerDetailList(checkID, out errMsg);
                                List<int> ayList = new List<int>();
                                List<int> gunList = new List<int>();
                                foreach (var dtlItem in timerDetailList.Where(x => x.Value1 == "a"))
                                {
                                    ayList.Add(Convert.ToInt32(dtlItem.Value2));
                                }

                                foreach (var dtlItem in timerDetailList.Where(x => x.Value1 == "g"))
                                {
                                    gunList.Add(Convert.ToInt32(dtlItem.Value2));
                                }

                                foreach (var ayItem in ayList)
                                {
                                    foreach (var gunItem in gunList)
                                    {
                                        DateTime myDate = new DateTime(DateTime.Now.Year, ayItem, gunItem, checkTime.Hour, checkTime.Minute, 0);
                                        if (currentDate <= myDate)
                                        {
                                            TimeList timeItem = new TimeList();
                                            timeItem.CheckTime = myDate;
                                            timeItem.Used = false;
                                            GeneralBussines.OperationTimeList.Add(timeItem);
                                        }
                                    }
                                }
                            } break;


                        default:
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
        }

        public static bool DeleteTimer(int _mainId, out string errMsg)
        {
            errMsg = string.Empty;
            DAL.ACCESSDB.DataHandler myDataHandler = new DAL.ACCESSDB.DataHandler();
            return myDataHandler.DeleteTimer(_mainId, out errMsg);
        }

        public static List<MODEL.ACCESSDB.Timer> GetTimerMainList(out string errMsg)
        {
            errMsg = string.Empty;
            DAL.ACCESSDB.DataHandler myDataHandler = new DAL.ACCESSDB.DataHandler();
            return myDataHandler.GetTimeList(out errMsg);
        }

        public static MODEL.ACCESSDB.Timer GetTimerById(int _mainId, out string errMsg)
        {
            errMsg = string.Empty;
            DAL.ACCESSDB.DataHandler myDataHandler = new DAL.ACCESSDB.DataHandler();
            return myDataHandler.GetTimerById(_mainId, out errMsg);
        }

        public static List<MODEL.ACCESSDB.TimerDetail> GetTimerDetailList(int _mainId, out string errMsg)
        {
            errMsg = string.Empty;
            DAL.ACCESSDB.DataHandler myDataHandler = new DAL.ACCESSDB.DataHandler();
            return myDataHandler.GetTimerDetailList(_mainId, out errMsg);
        }

        public static List<MODEL.ACCESSDB.VV_Timer> GetTimeViewList(out string errMsg)
        {
            errMsg = string.Empty;
            DAL.ACCESSDB.DataHandler myDataHandler = new DAL.ACCESSDB.DataHandler();
            return myDataHandler.GetTimeViewList(out errMsg);
        }

        public static bool SaveTimer(int _mainId, int _type, DateTime _startTime, List<V3ToLogo.MODEL.GENERAL.StringArray2D> valueList, out string errMsg)
        {
            bool returnValue = false;
            errMsg = string.Empty;
            DAL.ACCESSDB.DataHandler myDataHandler = new DAL.ACCESSDB.DataHandler();
            MODEL.ACCESSDB.Timer mainItem = new MODEL.ACCESSDB.Timer();
            mainItem.ID = _mainId;
            mainItem.Type_ = _type;
            mainItem.StartTime = _startTime;
            try
            {
                if (_mainId > 0)
                {
                    returnValue = myDataHandler.UpdateTimer(mainItem, out errMsg);
                }
                else
                {
                    mainItem.ID = myDataHandler.InsertTimer(mainItem, out errMsg);
                }

                if (mainItem.Type_ == 2 || mainItem.Type_ == 3)
                {
                    foreach (var item in valueList)
                    {
                        MODEL.ACCESSDB.TimerDetail tdItem = new MODEL.ACCESSDB.TimerDetail();
                        tdItem.TimerId = mainItem.ID;
                        tdItem.Value1 = item.value1;
                        tdItem.Value2 = item.value2;
                        myDataHandler.InsertTimerDetail(tdItem, out errMsg);
                    }
                }
                returnValue = true;
            }
            catch (Exception ex)
            {
                returnValue = false;
                errMsg = ex.Message;
            }

            return returnValue;
        }
    }
}
