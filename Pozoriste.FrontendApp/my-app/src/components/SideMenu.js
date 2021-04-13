import React from 'react'
import '../style/style.css'
import { Link } from 'react-router-dom'
import { getRole } from './globalStorage/RoleCheck'
import { isUserLogged } from './globalStorage/IsUserLogged'
import { AddActorContext } from '../App'
import { AddTheatreContext } from '../App'
import { useContext } from 'react'
import AddTheatre from './Admin/TheatreActions/AddTheatre'

const SideMenu = () => {
    const [context, setContext] = useContext(AddActorContext, AddTheatreContext);

    return (
        <div className='side-menu'>
            <Link className='side-menu-element' to="/showlist">Predstave</Link>
            <Link className='side-menu-element' to="/piecealllist">Pozorisni komadi</Link>
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/addactor">Dodaj glumca</Link>
            }
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/actorlist">Svi glumci</Link>
            }
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/addshow">Dodaj predstavu</Link>
            }
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/addtheatre">Dodaj pozoriste</Link>
            }
            {
                (isUserLogged() && getRole() === 'admin') && <Link className='side-menu-element' to="/showalltheatres">Pozorista</Link>
            }
        </div>
    )
}

export default SideMenu
