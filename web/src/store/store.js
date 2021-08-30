import { configureStore } from "@reduxjs/toolkit";
import contactsReducer from './contacts/contacts.slice';

export default configureStore({
    reducer: {
        contacts: contactsReducer
    }
});

