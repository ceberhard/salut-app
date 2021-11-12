import { createSlice, createSelector, createAsyncThunk, createEntityAdapter } from "@reduxjs/toolkit";
import { getContacts } from '../../services/WebAPI.service';

const contactsAdapter = createEntityAdapter();
const initialState = contactsAdapter.getInitialState({ status: 'idle' });

export const fetchContacts = createAsyncThunk('contacts', async () => {
    console.log('fetchContacts: Initialize');
    const contacts = await getContacts();
    return contacts;
});

export const contactsSlice = createSlice({
    name: 'contacts',
    initialState,
    reducers: {
        contactAdded(state, action) {
            state.push(action.payload);
        },
    },
    extraReducers: (builder) => {
        builder
            .addCase(fetchContacts.pending, (state, action) => {
                state.status = 'loading';
            })
            .addCase(fetchContacts.fulfilled, (state, action) => {
                contactsAdapter.setAll(state, action.payload);
                state.status = 'idle';
            })
    }
});

export const { contactAdded }  = contactsSlice.actions;

export default contactsSlice.reducer;

export const { selectAll: selectContacts, selectById: selectContactById } = contactsAdapter.getSelectors((state) => state.contacts);

export const selectContactIds = createSelector(selectContacts, (contacts) => contacts.map((contact) => contact.id));
