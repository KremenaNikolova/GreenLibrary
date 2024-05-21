import { Link, useNavigate } from "react-router-dom";
import { DropdownMenu, DropdownItem, Dropdown, Icon } from "semantic-ui-react";
import './styles/dropDownProfile.css';

export default function DropDownProfile({ logout }) {
    const navigate = useNavigate();

    const handleNavigation = (path, state) => () => {
        navigate(path, { state });
    };

    return (
        <Dropdown item icon={<Icon name="user circle" size="big" />}>
            <DropdownMenu className="profile-dropdown-container">
                <DropdownItem onClick={handleNavigation("/user/settings", { activeItem: 'profile' })}>
                    Моят профил
                </DropdownItem>
                <DropdownItem onClick={handleNavigation("/user/settings", { activeItem: 'articles' })}>
                    Моите статии
                </DropdownItem>
                <DropdownItem onClick={handleNavigation("/user/settings", { activeItem: 'following' })}>
                    Потребители, които следвам
                </DropdownItem>
                <DropdownItem onClick={handleNavigation("/user/settings", { activeItem: 'followers' })}>
                    Последователи
                </DropdownItem>
                <DropdownItem onClick={logout} as={Link} to="/">
                    Изход
                </DropdownItem>
            </DropdownMenu>
        </Dropdown>
    );
}
