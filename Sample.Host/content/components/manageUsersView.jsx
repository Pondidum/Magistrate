var ManageUsersView = React.createClass({

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

  onUserCreated(user) {
    var newCollection = this.state.users.concat([user]);

    this.setState({
      users: newCollection
    });
  },

  onUserSelection(selected) {
    var newTotal = this.state.selected + (selected ? 1 : -1);

    this.setState({
      selected: newTotal
    });
  },

  render() {

    var onUserSelection = this.onUserSelection;
    var noSelection = this.state.selected <= 0;
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
              <li><CreateUserDialog onUserCreated={this.onUserCreated} /></li>
              <li><AddPermissionsDialog noSelection={noSelection} /></li>
              <li><AddRolesDialog noSelection={noSelection} /></li>
            </ul>

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
