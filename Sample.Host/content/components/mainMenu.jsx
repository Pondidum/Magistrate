var MainMenu = React.createClass({

  render() {

    var router = this.props.router;

    var userLink = "#" +  router.generate('users');
    var roleLink = "#" +  router.generate('roles');
    var permLink = "#" +  router.generate('permissions');

    return (
      <div className="col-md-3">
        <div className="list-group">

          <a className="list-group-item active" href={userLink}>
            <h4 className="list-group-item-heading">Users</h4>
            <p className="list-group-item-text">View and manage users known to Magistrate.</p>
          </a>

          <a className="list-group-item" href={roleLink}>
            <h4 className="list-group-item-heading">Roles</h4>
            <p className="list-group-item-text">Manage Roles (groups of permissions.)</p>
          </a>

          <a className="list-group-item" href={permLink}>
            <h4 className="list-group-item-heading">Permissions</h4>
            <p className="list-group-item-text">Add, modify and remove permissions.</p>
          </a>

        </div>
      </div>
    );
  }

});
