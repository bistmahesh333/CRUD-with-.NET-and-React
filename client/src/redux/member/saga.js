import {call, put, takeEvery} from "redux-saga/effects"
import { actionTypes } from "./action";
import { httpGetMemberType, httpGetMemberReport } from "../http/member/MemberHttp";


function* GetMemberType(){
    try {
    const list = yield call(httpGetMemberType);
    const data = list.data.ResponseData;
    yield put({ type: actionTypes.SET_MEMBER_TYPE, data });
  } catch (e) {
    console.log("error", e.message);
  }
}



function* GetMemberReport(action){
      try {
    const formData = action.data || {};
    const { memberId = 0, memberTypeId = 0 } = formData;

    const response = yield call(() => httpGetMemberReport(memberId, memberTypeId));
    const data = response.data;

    if (data.length === 0) {
      console.warn("No member data returned.");
    }

    yield put({ type: actionTypes.SET_MEMBER_REPORT, data });
  } catch (e) {
    console.log("error", e.message);
  }
}


function* MemberReportSaga() {

    yield takeEvery(actionTypes.GET_MEMBER_TYPE, GetMemberType);
    yield takeEvery(actionTypes.GET_MEMBER_REPORT, GetMemberReport);
}

export default MemberReportSaga;