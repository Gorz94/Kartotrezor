import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { Map } from '../../model/Map'

export interface MapState {
    map: Map | undefined,
    id: string,
    finished: boolean
}

const initialState: MapState = {
  map: undefined,
  id: '',
  finished: false
}

export const mapSlice = createSlice({
  name: 'mapSlice',
  initialState,
  reducers: {
    INIT_MAP: (state: MapState, action: PayloadAction<{command: string}>) => state,
    INIT_MAP_RESULT: (state: MapState, action: PayloadAction<{success: boolean, id: string, error: string}>) => {
        if (action.payload.success) {
            state.id = action.payload.id;
        } else {
            console.log('Erreur: ' + action.payload.error);
        }
    },
    CONTINUE_MAP: (state: MapState) => state,
    CONTINUE_MAP_RESULT: (state: MapState, action: PayloadAction<{success: boolean, finished: boolean, map: Map, error: string}>) => {
        if (action.payload.success) {
            state.map = action.payload.map;
            state.finished = action.payload.finished;
        } else {
            console.log('Erreur: ' + action.payload.error);
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