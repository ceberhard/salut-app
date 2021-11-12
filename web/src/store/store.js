import { configureStore } from "@reduxjs/toolkit";
import contactsReducer from './contacts/contacts.slice';
import gamesystemReducer from './gamesystem/gamesystem.slice';

export default configureStore({
    reducer: {
        contacts: contactsReducer,
        gamesystem: gamesystemReducer
    }
});

