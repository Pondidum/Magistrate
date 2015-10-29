var UsersView = React.createClass({

  getInitialState() {
    return {
      filter: "",
      users: [],
      selected: 0
    };
  },

  componentDidMount() {
    this.getUsers();
  },

  getUsers() {

    $.ajax({
      url: "/api/users/all",
      dataType: 'json',
      cache: false,
      success: function(data) {
        this.setState({
          users: data || []
        });
      }.bind(this),
      error: function(xhr, status, err) {
        console.error(this.props.url, status, err.toString());
      }.bind(this)
    });

  },

  filterChanged(value) {
    this.setState({
      filter: value
    });
  },

  onUserSelection(selected) {
    var newTotal = this.state.selected + (selected ? 1 : -1);

    this.setState({
      selected: newTotal
    });
  },

  openPermissions(e) {
    e.preventDefault();
    this.refs.permissionsDialog.open()
  },

  openRoles(e) {
    e.preventDefault();
    this.refs.rolesDialog.open();
  },

  render() {

    var onUserSelection = this.onUserSelection;
    var noSelection = this.state.selected <= 0 ? " disabled" : "";
    var filter = new RegExp(this.state.filter, "i");

    var users = this.state.users
      .filter(function(user) {
        return user.name.search(filter) != -1;
      })
      .map(function(user, index) {
        return (
          <UserTile key={index} user={user} onChange={onUserSelection} />
        );
      });

    return (
      <div>
        <div className="row" style={{ marginBottom: "1em" }}>
          <div className="col-md-7">

            <ul className="list-unstyled list-inline">
              <li><a href="#" className="btn btn-primary">Create User</a></li>
              <li><a href="#" className={"btn btn-default" + noSelection} onClick={this.openPermissions}>Add Permission</a></li>
              <li><a href="#" className={"btn btn-default" + noSelection} onClick={this.openRoles}>Add Role</a></li>
            </ul>

            <Dialog title="Select Permissions" onSubmit={() => console.log("permissionsDialog.submit") } ref="permissionsDialog">
              <p>List of all Permissions</p>
            </Dialog>

            <Dialog title="Select Roles" onSubmit={() => console.log("rolesDialog.submit") } ref="rolesDialog">
              <p>List of all Roles</p>
            </Dialog>

          </div>
          <FilterBar className="pull-right col-md-5" filterChanged={this.filterChanged} />
        </div>
        <div className="row">
            {users}
        </div>
      </div>
    );
  }

});
