import React from 'react';
import { useSelector, useDispatch } from 'react-redux';
import {
    contactAdded,
    selectContactIds
} from '../store/contacts/contacts.slice';
import styles from './Contact.module.css';
import { ContactItem } from './ContactItem';

export default function ContactsList() {
    console.log('In ContactsList...');
    
    // const contacts = useSelector((state) => state.contacts);
    const contactIds = useSelector(selectContactIds);

    console.log('ContactsList data', contactIds);

    const dispatch = useDispatch();

    const ContactItems = contactIds.map((contactId) =>
        <ContactItem key={contactId} id={contactId} />
    );

    return (
        <div>
            <button aria-label="Click Me" onClick={() => dispatch(contactAdded({ id: '2', firstName: 'Sara', lastName: 'Connolly' }))}>Click Me</button>
            {ContactItems}
        </div>
    );
}