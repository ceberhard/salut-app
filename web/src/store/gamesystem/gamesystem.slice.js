import { createSlice, createSelector, createAsyncThunk, createEntityAdapter } from "@reduxjs/toolkit";
import { act } from "react-dom/test-utils";
import { buildGame } from '../../services/WebAPI.service';

const gamesystemAdapter = createEntityAdapter({
    selectId: (entity) => entity.gameSystemId
});
const initialState = gamesystemAdapter.getInitialState({ status: 'idle' });

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
                gamesystemAdapter.setAll(state, action.payload);
                state.status = 'idle';
            });
    }
});

export default gamesystemSlice.reducer;

export const { selectById: buildGameSystemById } = gamesystemAdapter.getSelectors((state) => state.gamesystem);

export const buildGameSystem = createSelector(buildGameSystemById, (gamesystem) => {
    console.log('selector GameSystem', gamesystem);

    return gamesystem;
});
