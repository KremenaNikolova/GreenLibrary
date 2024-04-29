import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import {
    DropdownMenu, DropdownItem, Dropdown
} from 'semantic-ui-react';
import './styles/dropDownCategories.css';


export default function DropDownCategories() {
    const [categories, setCategories] = useState([]);

    useEffect(() => {
        axios.get("https://localhost:7195/api/categories").then((response) => {
            setCategories(response.data);
        });
    }, []);

  return (
      <Dropdown item text='Категории'>
          <DropdownMenu>
              {categories.map((category) => (
                  <DropdownItem key={category.id} id='dropdownmenu' as={Link} to={`/categories/${category.name}`}>
                      {category.name}
                  </DropdownItem>
              ))}
          </DropdownMenu>
      </Dropdown>
  );
}
