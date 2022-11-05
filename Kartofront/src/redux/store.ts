import { applyMiddleware, combineReducers, createStore } from 'redux';
import { composeWithDevTools } from 'redux-devtools-extension';

import createSagaMiddleware from "redux-saga";
import mapSaga from './sagas/mapSaga';
import mapSlice from './slices/mapSlice';
import { all } from "redux-saga/effects";

const sagaMiddleware = createSagaMiddleware();

function * mainSaga() {
  yield all([
    mapSaga()
  ]);
}

const reducers = combineReducers({
    map: mapSlice
});

const store = createStore(
  reducers,
  composeWithDevTools(applyMiddleware(sagaMiddleware)),
)

sagaMiddleware.run(mainSaga);

export default store;

export type RootState = ReturnType<typeof reducers> 