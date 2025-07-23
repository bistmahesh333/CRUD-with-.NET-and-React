import { call, put, takeLatest } from 'redux-saga/effects';
import { GET_STUDENTS_REQUEST, getStudentsSuccess, getStudentsFailure } from './action';
import { fetchStudentsApi } from '../http/student/api';

function* fetchStudentsSaga() {
  try {
    const response = yield call(fetchStudentsApi);
    yield put(getStudentsSuccess(response.data));
  } catch (error) {
    yield put(getStudentsFailure(error.message));
  }
}

export default function* studentSaga() {
  yield takeLatest(GET_STUDENTS_REQUEST, fetchStudentsSaga);
}
