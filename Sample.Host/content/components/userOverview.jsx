var UserOverview = React.createClass({

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

  onUserRemoved(user) {

    var newCollection = this.state.users.filter(function(u) {
      return u.key !== user.key
    });

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

    var self = this;
    var onUserSelection = this.onUserSelection;
    var noSelection = this.state.selected <= 0;
    var filter = new RegExp(this.state.filter, "i");

    var users = this.state.users
      .filter(function(user) {
        return user.name.search(filter) != -1;
      })
      .map(function(user, index) {
        return (
          <UserTile key={index} user={user} onChange={onUserSelection} onUserRemoved={self.onUserRemoved} navigate={self.props.navigate} />
        );
      });

    return (
      <div>
        <OverviewActionBar filterChanged={this.filterChanged}>
          <li><CreateUserDialog onUserCreated={this.onUserCreated} /></li>
          <li><AddPermissionsDialog noSelection={noSelection} /></li>
          <li><AddRolesDialog noSelection={noSelection} /></li>
        </OverviewActionBar>
        <div className="col-sm-9">
          <div className="row">
            <FilterBar className="pull-right col-sm-5" filterChanged={this.props.filterChanged} />
          </div>
          <div className="row">
            {users}
          </div>
        </div>
      </div>
    );
  }

});
