import { useNavigate } from "react-router-dom";
import { DropdownMenu, DropdownItem, Dropdown, Divider } from "semantic-ui-react";
import './styles/dropDownAdminMenu.css';

export default function DropDownAdminMenu({ user }) {
    const navigate = useNavigate();

    const handleNavigation = (path, state) => () => {
        navigate(path, { state });
    };

    return (
        <Dropdown item text='Админ панел' className="admin-dropdown-container">
            <DropdownMenu className="dropdown-link-container">
                <DropdownItem className="dropdown-link" onClick={handleNavigation("/admin/articles")}>
                    Одобряване на статии
                </DropdownItem>
                {user && user.roles === 'Admin' && (
                    <>
                        <Divider className="admin-divider" />
                        <DropdownItem className="dropdown-link admin" onClick={handleNavigation("/user/settings")}>
                            Потребители
                        </DropdownItem>
                        <Divider className="admin-divider" />
                        <DropdownItem className="dropdown-link" onClick={handleNavigation("/user/settings")}>
                            Екип
                        </DropdownItem>
                    </>
                )}
            </DropdownMenu>
        </Dropdown>
    );
}