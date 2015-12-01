var UserOverview = React.createClass({

  getInitialState() {
    return {
      filter: "",
      users: [],
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

  onFilterChanged(value) {
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

  render() {

    var self = this;
    var noSelection = this.state.selected <= 0;
    var filter = new RegExp(this.state.filter, "i");

    var users = this.state.users
      .filter(function(user) {
        return user.name.search(filter) != -1;
      })
      .map(function(user, index) {
        return (
          <UserTile key={index} user={user} onUserRemoved={self.onUserRemoved} navigate={self.props.navigate} />
        );
      });

    var create = (<CreateUserDialog onUserCreated={this.onUserCreated} />);

    return (
      <div>
        <ContentArea filterChanged={this.onFilterChanged} actions={[create]}>
          {users}
        </ContentArea>
      </div>
    );
  }

});
