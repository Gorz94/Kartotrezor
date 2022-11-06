import { all, call, delay, put,select, takeEvery } from 'redux-saga/effects'
import { PayloadAction } from '@reduxjs/toolkit'
import { CONTINUE_MAP, CONTINUE_MAP_RESULT, INIT_MAP, INIT_MAP_RESULT, MapState } from '../slices/mapSlice';
import { RootState } from '../store';
import { contiuneMap, initMap } from '../services/mapService';
import { Map } from '../../model/Map';


export default function* mapSaga() {
    yield all([
        takeEvery(INIT_MAP, initSaga),
        takeEvery(CONTINUE_MAP, continueSaga),
        takeEvery(INIT_MAP_RESULT, continueSaga)
    ]);
}

function* initSaga (action: PayloadAction<{command: string}>) {
    try {
        yield put(INIT_MAP_RESULT(yield call(initMap, action.payload.command)));
    } catch  (e) {
        console.error(e);
    }
}

function* continueSaga () {
    try {
        const state: MapState = yield select((s: RootState) => s.map);

        if ((!state.finished || state.map == undefined) && state.id.length > 0) {
            const result: {success: boolean, finished: boolean, map: Map, error: string} = yield call(contiuneMap, state.id);
            yield put(CONTINUE_MAP_RESULT(result));

            if (result.success) {
                yield delay(600);
                yield put(CONTINUE_MAP());
            }
        }
    } catch  (e) {
        console.error(e);
    }
}