import { createSlice, createSelector, createAsyncThunk } from "@reduxjs/toolkit";
import { buildGame } from '../../services/WebAPI.service';

const initialState = { status: 'idle', gamesystem: {} };

export const fetchGameSystem = createAsyncThunk('gamesystem', async () => {
    console.log('fetchGameSystem: Initialize');
    const gs = await buildGame();
    return gs;
});

export const gamesystemSlice = createSlice({
    name: "gamesystem",
    initialState,
    extraReducers: (builder) => {
        builder
            .addCase(fetchGameSystem.pending, (state, action) => {
                state.status = 'loading';
            })
            .addCase(fetchGameSystem.fulfilled, (state, action) => {
                console.log('gamesystemSlice data', action.payload);
                state.gamesystem = action.payload;
                state.status = 'idle';
            });
    }
});

export default gamesystemSlice.reducer;

const selectGameSystem = (state) => state.gamesystem || {};
export const buildGameSystem = createSelector(selectGameSystem, (state) => state.gamesystem);
