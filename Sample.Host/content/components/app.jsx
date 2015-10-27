var App = React.createClass({

  router: unirouter({
    users: 'GET /users',
    roles: 'GET /roles',
    permissions: 'GET /permissions'
  }),

  getInitialState() {
    return {
      location: this.getLocation()
    };
  },

  getLocation() {
    var location = window.location.hash.replace(/^#\/?|\/$/g, '');

    return this.router.lookup(location);
  },

  navigated() {
    this.setState({
      location: this.getLocation()
    })
  },

  componentDidMount() {
    window.addEventListener('hashchange', this.navigated, false);
  },

  render() {

    return (
      <div className="row">
        <MainMenu router={this.router} />
        <div className="col-md-9">Content</div>
      </div>
    );
  }

});
