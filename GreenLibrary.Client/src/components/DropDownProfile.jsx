import { Link } from "react-router-dom";
import { DropdownMenu, DropdownItem, Dropdown, Icon } from "semantic-ui-react";
import './styles/dropDownProfile.css';

export default function DropDownProfile({ logout }) {
    return (
        <Dropdown item icon={<Icon name="user circle" size="big" />}>
            <DropdownMenu className="profile-dropdown-container">
                <DropdownItem as={Link} to="/user/settings">
                    Моят профил
                </DropdownItem>
                <DropdownItem as={Link} to="#">
                    Моите статии
                </DropdownItem>
                <DropdownItem as={Link} to="#">
                    Потребители, които следвате
                </DropdownItem>
                <DropdownItem as={Link} to="#">
                    Последователи
                </DropdownItem>
                <DropdownItem onClick={logout} as={Link} to="#">
                    Изход
                </DropdownItem>
            </DropdownMenu>
        </Dropdown>
    );
}
