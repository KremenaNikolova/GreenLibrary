import React from 'react';
import { Dropdown } from 'semantic-ui-react';
import './styles/sortArticles.css'

export default function SortArticles({ onSortChange, user }) {
    const sortOptions = [
        { key: 'title-asc', value: 'title-asc', text: 'Сортирай по: Заглавие (А-З)' },
        { key: 'title-desc', value: 'title-desc', text: 'Сортирай по: Заглавие (З-А)' },
        { key: 'createdon-desc', value: 'createon-newest', text: 'Сортирай по: Дата (Първо най-новите)' },
        { key: 'createdon-asc', value: 'createon-oldest', text: 'Сортирай по: Дата (Първо най-старите)' },
    ];

    if (user && (user.roles === 'Admin' || user.roles === 'Moderator')) {
        sortOptions.push({ key: 'approved', value: 'approved', text: 'Сортирай по: Статии, които не са одобрени' });
    }

    const handleChange = (e, { value }) => {
        const sortBy = value;
        onSortChange(sortBy);
    };

    return (
        <Dropdown
            className = 'sort-by-dropdown'
            placeholder='Сортирай по:'
            selection
            options={sortOptions}
            onChange={handleChange}
        />
    );
}