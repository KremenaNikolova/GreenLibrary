import React from 'react';
import { Dropdown } from 'semantic-ui-react';
import './styles/sortArticles.css'

export default function SortUsers({ onSortChange }) {
    const sortOptions = [
        { key: 'username-asc', value: 'username-asc', text: 'Сортирай по: Потребителско име' },
        { key: 'firstname-asc', value: 'firstname-asc', text: 'Сортирай по: Име' },
        { key: 'lastname-asc', value: 'lastname-asc', text: 'Сортирай по: Фамилия' },
        { key: 'createdon-desc', value: 'createon-newest', text: 'Сортирай по: Дата (Първо най-новите)' },
        { key: 'createdon-asc', value: 'createon-oldest', text: 'Сортирай по: Дата (Първо най-старите)' },
        { key: 'moderators', value: 'moderators', text: 'Сортирай по: Модератори' },
    ];

    const handleChange = (e, { value }) => {
        const sortBy = value;
        onSortChange(sortBy);
    };

    return (
        <Dropdown
            className='sort-by-dropdown'
            placeholder='Сортирай по:'
            selection
            options={sortOptions}
            onChange={handleChange}
        />
    );
}