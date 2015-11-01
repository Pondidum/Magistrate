var MainMenu = React.createClass({

  navigate(route, e) {
    e.preventDefault();
    this.props.navigate(route);
  },

  render() {

    var selected = this.props.selected;

    return (
      <div className="col-md-3">
        <div className="list-group">

          <a className={"list-group-item " + (selected == "users" ? "active" : "")} href="#" onClick={this.navigate.bind(this, 'users')}>
            <h4 className="list-group-item-heading">Users</h4>
            <p className="list-group-item-text">View and manage users known to Magistrate.</p>
          </a>

          <a className={"list-group-item " + (selected == "roles" ? "active" : "")} onClick={this.navigate.bind(this, 'roles')}>
            <h4 className="list-group-item-heading">Roles</h4>
            <p className="list-group-item-text">Manage Roles (groups of permissions.)</p>
          </a>

          <a className={"list-group-item " + (selected == "permissions" ? "active" : "")} onClick={this.navigate.bind(this, 'permissions')}>
            <h4 className="list-group-item-heading">Permissions</h4>
            <p className="list-group-item-text">Add, modify and remove permissions.</p>
          </a>

        </div>
      </div>
    );
  }

});
