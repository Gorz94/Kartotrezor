import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { Map } from '../../model/Map'

export interface MapState {
    map: Map | undefined,
    id: string,
    finished: boolean,
    error: string
}

const initialState: MapState = {
  map: undefined,
  id: '',
  error: '',
  finished: false
}

export const mapSlice = createSlice({
  name: 'mapSlice',
  initialState,
  reducers: {
    INIT_MAP: (state: MapState, action: PayloadAction<{command: string}>) => {
      state.id = '';
      state.error = '';
    },
    INIT_MAP_RESULT: (state: MapState, action: PayloadAction<{success: boolean, id: string, error: string}>) => {
        if (action.payload.success) {
            state.id = action.payload.id;
            state.error = '';
        } else {
            console.log('Erreur: ' + action.payload.error);
            state.id = '';
            state.error = action.payload.error;
        }
    },
    CONTINUE_MAP: (state: MapState) => state,
    CONTINUE_MAP_RESULT: (state: MapState, action: PayloadAction<{success: boolean, finished: boolean, map: Map, error: string}>) => {
        if (action.payload.success) {
            state.map = action.payload.map;
            state.finished = action.payload.finished;
            state.error = '';

            if (action.payload.finished) {
              state.id = '';
            }
        } else {
            console.log('Erreur: ' + action.payload.error);
            state.id = '';
            state.error = action.payload.error;
        }
    }
  }
})

export const { 
  INIT_MAP,
  INIT_MAP_RESULT,
  CONTINUE_MAP,
  CONTINUE_MAP_RESULT
} = mapSlice.actions

export default mapSlice.reducer