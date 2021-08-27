import { createSlice, createSelector, createAsyncThunk, createEntityAdapter } from "@reduxjs/toolkit";
import { getContacts } from '../../services/WebAPI.service';

const contactsAdapter = createEntityAdapter();
const initialState = contactsAdapter.getInitialState({ status: 'idle' });
/*
const initialState = [
    { id: '1', firstName: 'Chris', lastName: 'Eberhard' },
    { id: '2', firstName: 'Sara', lastName: 'Connolly' },
    { id: '3', firstName: 'Gus', lastName: 'Eberhard' }
];
*/

export const fetchContacts = createAsyncThunk('contacts/fetchContacts', async () => {
    console.log('fetchContacts: Initialize');
    const contacts = await getContacts();
    console.log('fetchContacts: Found ' + contacts.length + ' Contacts');
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



